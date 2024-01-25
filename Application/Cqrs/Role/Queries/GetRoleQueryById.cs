using Application.Common.Response;
using Application.DTOs.Role;
using Application.Interfaces.Roles;
using MediatR;

namespace Application.Cqrs.Role.Queries
{
    public class GetRoleQueryById : IRequest<ApiResponse<RoleDto>>
    {
        public Guid Id { get; set; }
    }
    public class GetRoleQueryByIdHandler : IRequestHandler<GetRoleQueryById, ApiResponse<RoleDto>>
    {
        private readonly IRolesService _rolesService;
        public GetRoleQueryByIdHandler(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public async Task<ApiResponse<RoleDto>> Handle(GetRoleQueryById request, CancellationToken cancellationToken)
        {
            return await _rolesService.GetRolesById(request);
        }
    }
}
