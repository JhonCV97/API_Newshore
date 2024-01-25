using Application.DTOs.Role;
using Application.DTOs.User;
using AutoMapper;
using Domain.Models.Role;
using Domain.Models.User;

namespace Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserPostDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<Role, RolePostDto>();

        }
    }
}
