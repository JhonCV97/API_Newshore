using Application.Common.Response;
using Application.Cqrs.User.Queries;
using Application.DTOs.Journey;
using Application.DTOs.User;
using Application.Interfaces.Journey;
using Application.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Journey.Queries
{
    public class GetRouteQuery : IRequest<ApiResponse<JourneyDto>>
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public bool LongestRoute { get; set; } = false;
    }
    public class GetRouteQueryHandler : IRequestHandler<GetRouteQuery, ApiResponse<JourneyDto>>
    {
        private readonly IJourneyService _journeyService;
        public GetRouteQueryHandler(IJourneyService journeyService)
        {
            _journeyService = journeyService;
        }

        public async Task<ApiResponse<JourneyDto>> Handle(GetRouteQuery request, CancellationToken cancellationToken)
        {
            return await _journeyService.GetRoute(request);
        }
    }
}
