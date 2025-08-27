using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.Employer;
using WorkFinder.ServiceContracts.DTOs.Skill;
using WorkFinder.ServiceContracts.DTOs.User;

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

            //Employers
            CreateMap<EmployerRequestDto, Employer>();
            CreateMap<EmployerRequestDto, RegisterRequestDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName));

            //Skills
            CreateMap<SkillRequestDto, Skill>();
            CreateMap<Skill, SkillResponseDto>();
            CreateMap<string?, Skill>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src));
        }
    }
}
