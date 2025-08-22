using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.ServiceContracts.DTOs;

namespace WorkFinder.Services.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Users
            CreateMap<UserRequestDto, User>();
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.RoleId))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
        }
    }
}
