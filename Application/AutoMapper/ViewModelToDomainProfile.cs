using Application.DTOs.Role;
using Application.DTOs.User;
using AutoMapper;
using Domain.Models.Role;
using Domain.Models.User;

namespace Application.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserPostDto, User>();
            CreateMap<RoleDto, Role>();
            CreateMap<RolePostDto, Role>();

        }
    }
}
