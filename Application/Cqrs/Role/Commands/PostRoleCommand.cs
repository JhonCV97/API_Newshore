using Application.Common.Response;
using Application.DTOs.Role;
using Application.Interfaces.Roles;
using MediatR;

namespace Application.Cqrs.Role.Commands
{
    public class PostRoleCommand : IRequest<ApiResponse<RoleDto>>
    {
        public RolePostDto RolePostDto { get; set; }
    }
    public class PostRoleCommandHandler : IRequestHandler<PostRoleCommand, ApiResponse<RoleDto>>
    {
        private readonly IRolesService _rolesService;
        public PostRoleCommandHandler(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public async Task<ApiResponse<RoleDto>> Handle(PostRoleCommand request, CancellationToken cancellationToken)
        {
            return await _rolesService.AddRole(request);
        }
    }
}
