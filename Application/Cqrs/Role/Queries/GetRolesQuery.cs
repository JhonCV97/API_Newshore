using Application.Common.Response;
using Application.DTOs.Role;
using Application.Interfaces.Roles;
using MediatR;

namespace Application.Cqrs.Role.Queries
{
    public class GetRolesQuery : IRequest<ApiResponse<List<RoleDto>>>
    {

    }
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, ApiResponse<List<RoleDto>>>
    {
        private readonly IRolesService _rolesService;
        public GetRolesQueryHandler(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public async Task<ApiResponse<List<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _rolesService.GetRoles();
        }
    }
}
