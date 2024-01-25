using Application.DTOs.Flight;
using Application.DTOs.Journey;
using Application.DTOs.JourneyFlight;
using Application.DTOs.Role;
using Application.DTOs.Transport;
using Application.DTOs.User;
using AutoMapper;
using Domain.Models.Flight;
using Domain.Models.Journey;
using Domain.Models.JourneyFlight;
using Domain.Models.Role;
using Domain.Models.Transport;
using Domain.Models.User;

namespace Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserPostDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<Role, RolePostDto>();
            CreateMap<Transport, TransportDto>();
            CreateMap<Journey, JourneyDto>();
            CreateMap<Journey, PostJourneyDto>();
            CreateMap<Flight, FlightDto>();
            CreateMap<Flight, PostFlightDto>();
            CreateMap<JourneyFlight, JourneyFlightDto>();

        }
    }
}
