using Application.Common.Exceptions;
using Application.Common.Response;
using Application.Cqrs.Journey.Queries;
using Application.DTOs.Flight;
using Application.DTOs.Journey;
using Application.DTOs.JourneyFlight;
using Application.DTOs.RequestFlight;
using Application.DTOs.Transport;
using Application.Interfaces.EncryptAndDecrypt;
using Application.Interfaces.Journey;
using Application.Interfaces.SearchAlgorithm;
using Application.Services.EncryptAndDecrypt;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models.Flight;
using Domain.Models.JourneyFlight;
using Domain.Models.Transport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;

namespace Application.Services.Journey
{
    public class JourneyService : IJourneyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly ISearchShortRouteService _searchShortRouteService;
        private readonly ISearchLongestRouteService _searchLongestRouteService;
        private readonly IEncryptAndDecryptService _encryptAndDecryptService;
        private string? _urlRequest;
        private static Dictionary<string, string> responseCache = new Dictionary<string, string>();
        public JourneyService(IUnitOfWork unitOfWork, IMapper autoMapper, IConfiguration config,
                             ISearchShortRouteService searchShortRouteService, ISearchLongestRouteService searchLongestRouteService,
                             IEncryptAndDecryptService encryptAndDecryptService)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _searchShortRouteService = searchShortRouteService;
            _searchLongestRouteService = searchLongestRouteService;
            _encryptAndDecryptService = encryptAndDecryptService;
            _urlRequest = config["UrlRequest"];
        }

        public async Task<ApiResponse<JourneyDto>> GetRoute(GetRouteQuery getRouteQuery)
        {
            var response = new ApiResponse<JourneyDto>();

            try
            {
                if (getRouteQuery.Destination.ToUpper().Equals(getRouteQuery.Origin.ToUpper()))
                {
                    throw new BadRequestException("La ruta de Origen y destino no pueden ser iguales");
                }

                var JourneyFlight = _unitOfWork.JourneyRepository.Get()
                                                                .Where(j => j.Destination == getRouteQuery.Destination.ToUpper())
                                                                .Where(j => j.Origin == getRouteQuery.Origin.ToUpper())
                                                                .Where(j => j.LongestRoute == getRouteQuery.LongestRoute)
                                                                    .Include(jf => jf.JourneyFlight)
                                                                    .ThenInclude(f => f.Flight)
                                                                    .ThenInclude(t => t.Transport)
                                                                .FirstOrDefault();

                
                var route = new JourneyDto();


                if (JourneyFlight == null)
                {
                    //Se desencripta la url de la api para peticion
                    _urlRequest = _encryptAndDecryptService.Decrypt(_urlRequest);

                    var listJourney = ListJourney(_urlRequest).Result;
                    route = await getNewRoute(getRouteQuery, listJourney);
                    Log.Information("GetRoute => {@route}", route);
                    await SaveRoutes(route, getRouteQuery.LongestRoute);
                }
                else
                {
                    var FligthsList = JourneyFlight.JourneyFlight.Select(x => x.Flight).ToList();

                    route.Origin = JourneyFlight.Origin;
                    route.Destination = JourneyFlight.Destination;
                    route.Price = JourneyFlight.Price;
                    route.Flights = _autoMapper.Map<List<FlightDto>>(FligthsList);
                }

                response.Data = route;
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al consultar el registro, consulte con el administrador. {ex.Message} ";                
                Log.Error("GetRoute => {@ex.Message}", ex.Message);
                throw new BadRequestException($"Error al consultar el registro. {ex.Message} ");
            }

            return response;
        }

        public async Task<JourneyDto> getNewRoute(GetRouteQuery getRouteQuery, List<RequestFlightDto> listRoutes)
        {
            var routes = listRoutes.Select(ra => new FlightDto
            {
                Origin = ra.arrivalStation,
                Destination = ra.departureStation,
                Price = ra.price,
                Transport = new TransportDto
                {
                    FlightCarrier = ra.flightCarrier,
                    FlightNumber = ra.flightNumber
                }
            }).ToList();

            var result = new List<FlightDto>();

            if (getRouteQuery.LongestRoute)
            {
                result = _searchLongestRouteService.FindLongestRoute(routes, getRouteQuery);
            }
            else
            {
                result = _searchShortRouteService.FindShortestRoute(routes, getRouteQuery);
            }

            var TotalPrice = result.Select(r => r.Price).Sum();

            var Journey = new JourneyDto
            {
                Origin = getRouteQuery.Origin,
                Destination = getRouteQuery.Destination,
                Price = TotalPrice,
                Flights = result
            };

            return Journey;
        }

        static async Task<List<RequestFlightDto>> ListJourney(string apiUrl)
        {
            var ListJourney = new List<RequestFlightDto>();
            // Comprueba si la respuesta está en caché
            if (responseCache.TryGetValue(apiUrl, out var cachedResponse))
            {
                ListJourney = JsonConvert.DeserializeObject<List<RequestFlightDto>>(cachedResponse);
                return ListJourney;
            }

            // Si no está en caché, realiza la solicitud y almacena en caché la respuesta
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    responseCache[apiUrl] = responseData; // Almacena en caché la respuesta
                    ListJourney = JsonConvert.DeserializeObject<List<RequestFlightDto>>(responseData);
                }

                return ListJourney;
            }
        }

        private async Task<bool> SaveRoutes(JourneyDto journeyDto, bool LongestRoute)
        {
            try
            {
                var DataJourney = new PostJourneyDto
                {
                    Origin= journeyDto.Origin,
                    Destination= journeyDto.Destination,
                    Price = journeyDto.Price,
                    LongestRoute = LongestRoute
                };

                var MapJourney = _autoMapper.Map<Domain.Models.Journey.Journey>(DataJourney);
                var Journey = await _unitOfWork.JourneyRepository.Add(MapJourney);

                var response = await TouringFlights(journeyDto, Journey.Id);
                Log.Information("SaveRoutes => {@Journey}", Journey);
                return response;
            }
            catch (Exception ex)
            {
                Log.Error("SaveRoutes => {@ex.Message}", ex.Message);
                throw new BadRequestException($"Error al guardar las rutas, consulte con el administrador. {ex.Message} ");
            }
        }

        private async Task<bool> TouringFlights(JourneyDto journeyDto, Guid JourneyId)
        {
            try
            {
                foreach (var itemFlight in journeyDto.Flights)
                {
                    var transportDto = new TransportDto
                    {
                        FlightCarrier = itemFlight.Transport.FlightCarrier,
                        FlightNumber = itemFlight.Transport.FlightNumber
                    };

                    var transport = await _unitOfWork.TransportRepository.Add(_autoMapper.Map<Transport>(transportDto));

                    var flightDto = new PostFlightDto
                    {
                        Origin = itemFlight.Origin,
                        Destination = itemFlight.Destination,
                        Price = itemFlight.Price,
                        TransportId = transport.Id
                    };

                    var flight = await _unitOfWork.FlightRepository.Add(_autoMapper.Map<Flight>(flightDto));

                    var journeyFlightDto = new JourneyFlightDto
                    {
                        JourneyId = JourneyId,
                        FlightId = flight.Id,
                    };

                    await _unitOfWork.JourneyFlightRepository.Add(_autoMapper.Map<JourneyFlight>(journeyFlightDto));
                }

                return true;

            }
            catch (Exception ex)
            {
                Log.Error("TouringFlights => {@ex.Message}", ex.Message);
                throw new BadRequestException($"Error al guardar las rutas, consulte con el administrador. {ex.Message} ");
            }
            
        }

    }
}
