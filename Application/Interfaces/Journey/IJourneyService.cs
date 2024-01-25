using Application.Common.Response;
using Application.Cqrs.Journey.Queries;
using Application.DTOs.Journey;

namespace Application.Interfaces.Journey
{
    public interface IJourneyService
    {
        Task<ApiResponse<JourneyDto>> GetRoute(GetRouteQuery getRouteQuery);
    }
}