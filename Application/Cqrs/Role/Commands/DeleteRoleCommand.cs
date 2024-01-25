using Application.Common.Response;
using Application.Interfaces.Roles;
using MediatR;

namespace Application.Cqrs.Role.Commands
{
    public class DeleteRoleCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ApiResponse<bool>>
    {
        private readonly IRolesService _rolesService;
        public DeleteRoleCommandHandler(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _rolesService.DeleteRole(request);
        }
    }
}
