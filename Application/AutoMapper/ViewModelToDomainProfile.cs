using Application.DTOs.Flight;
using Application.DTOs.Journey;
using Application.DTOs.JourneyFlight;
using Application.DTOs.Role;
using Application.DTOs.Transport;
using Application.DTOs.User;
using AutoMapper;
using Domain.Models.Flight;
using Domain.Models.Journey;
using Domain.Models.JourneyFlight;
using Domain.Models.Role;
using Domain.Models.Transport;
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
            CreateMap<TransportDto, Transport>();
            CreateMap<JourneyDto, Journey>();
            CreateMap<PostJourneyDto, Journey>();
            CreateMap<FlightDto, Flight>();
            CreateMap<PostFlightDto, Flight>();
            CreateMap<JourneyFlightDto, JourneyFlight>();

        }
    }
}
