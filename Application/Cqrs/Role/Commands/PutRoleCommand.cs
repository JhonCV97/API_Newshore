using Application.Common.Response;
using Application.DTOs.Role;
using Application.Interfaces.Roles;
using MediatR;

namespace Application.Cqrs.Role.Commands
{
    public class PutRoleCommand : IRequest<ApiResponse<RoleDto>>
    {
        public RoleDto RoleDto { get; set; }
    }
    public class PutRoleCommandHandler : IRequestHandler<PutRoleCommand, ApiResponse<RoleDto>>
    {
        private readonly IRolesService _rolesService;
        public PutRoleCommandHandler(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public async Task<ApiResponse<RoleDto>> Handle(PutRoleCommand request, CancellationToken cancellationToken)
        {
            return await _rolesService.UpdateRole(request);
        }
    }
}
