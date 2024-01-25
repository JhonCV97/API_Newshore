using Application.Cqrs.Journey.Queries;
using Application.DTOs.Flight;

namespace Application.Interfaces.SearchAlgorithm
{
    public interface ISearchShortRouteService
    {
        List<FlightDto> FindShortestRoute(List<FlightDto> flights, GetRouteQuery request);
    }
}