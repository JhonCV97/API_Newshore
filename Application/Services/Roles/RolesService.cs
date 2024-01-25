using Application.Common.Exceptions;
using Application.Common.Response;
using Application.Cqrs.Role.Commands;
using Application.Cqrs.Role.Queries;
using Application.Cqrs.User.Commands;
using Application.Cqrs.User.Queries;
using Application.DTOs.Role;
using Application.Interfaces.Roles;
using AutoMapper;
using Domain.Interfaces;

namespace Application.Services.Roles
{
    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        public RolesService(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }

        public async Task<ApiResponse<List<RoleDto>>> GetRoles()
        {
            var response = new ApiResponse<List<RoleDto>>();

            try
            {
                response.Data = _autoMapper.Map<List<RoleDto>>(_unitOfWork.RoleRepository.Get().ToList());
                response.Result = true;
                response.Message = "OK";

                return response;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al consultar el registro, consulte con el administrador. {ex.Message} ";

                throw new BadRequestException($"Error al consultar el registro, consulte con el administrador. {ex.Message} ");
            }

        }

        public async Task<ApiResponse<RoleDto>> GetRolesById(GetRoleQueryById request)
        {
            var response = new ApiResponse<RoleDto>();

            try
            {
                response.Data = _autoMapper.Map<RoleDto>(await _unitOfWork.RoleRepository.GetById(request.Id));
                response.Result = true;
                response.Message = "OK";

                return response;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al consultar el registro, consulte con el administrador. {ex.Message} ";

                throw new BadRequestException($"Error al consultar el registro, consulte con el administrador. {ex.Message} ");
            }

        }


        public async Task<ApiResponse<RoleDto>> AddRole(PostRoleCommand request)
        {
            var response = new ApiResponse<RoleDto>();

            try
            {
                var Role = _autoMapper.Map<Domain.Models.Role.Role>(request.RolePostDto);

                response.Data = _autoMapper.Map<RoleDto>(await _unitOfWork.RoleRepository.Add(Role));
                response.Result = true;
                response.Message = "OK";

                return response;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al Crear Rol. {ex.Message} ";

                throw new BadRequestException($"Error al Crear Rol, consulte con el administrador. {ex.Message} ");
            }


        }

        public async Task<ApiResponse<RoleDto>> UpdateRole(PutRoleCommand request)
        {
            var response = new ApiResponse<RoleDto>();
            try
            {
                var roleDto = new RoleDto
                {
                    Id = request.RoleDto.Id,
                    Name = request.RoleDto.Name,
                    Status = request.RoleDto.Status
                };

                response.Data = _autoMapper.Map<RoleDto>(await _unitOfWork.RoleRepository.Put(_autoMapper.Map<Domain.Models.Role.Role>(roleDto)));
                response.Result = true;
                response.Message = "OK";

                return response;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al actualizar el registro, consulte con el administrador. {ex.Message} ";
                throw new BadRequestException($"Error al actualizar el registro, consulte con el administrador. {ex.Message} ");
            }

        }

        public async Task<ApiResponse<bool>> DeleteRole(DeleteRoleCommand request)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var Role = await _unitOfWork.RoleRepository.GetById(request.Id);

                response.Data = await _unitOfWork.RoleRepository.Delete(Role);
                response.Result = true;
                response.Message = "Ok";

                return response;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al eliminar el registro, consulte con el administrador. {ex.Message} ";
                throw new BadRequestException($"Error al eliminar el registro, consulte con el administrador. {ex.Message} ");
            }

        }
    }
}
