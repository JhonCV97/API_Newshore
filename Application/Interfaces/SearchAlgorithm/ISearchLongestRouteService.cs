using Application.Cqrs.Journey.Queries;
using Application.DTOs.Flight;

namespace Application.Interfaces.SearchAlgorithm
{
    public interface ISearchLongestRouteService
    {
        List<FlightDto> FindLongestRoute(List<FlightDto> routes, GetRouteQuery request);
    }
}