using Application.Common.Response;
using Application.Cqrs.Role.Commands;
using Application.Cqrs.Role.Queries;
using Application.Cqrs.User.Commands;
using Application.Cqrs.User.Queries;
using Application.DTOs.Role;

namespace Application.Interfaces.Roles
{
    public interface IRolesService
    {
        Task<ApiResponse<RoleDto>> AddRole(PostRoleCommand request);
        Task<ApiResponse<bool>> DeleteRole(DeleteRoleCommand request);
        Task<ApiResponse<List<RoleDto>>> GetRoles();
        Task<ApiResponse<RoleDto>> GetRolesById(GetRoleQueryById request);
        Task<ApiResponse<RoleDto>> UpdateRole(PutRoleCommand request);
    }
}