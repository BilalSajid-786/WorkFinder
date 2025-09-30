using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.ServiceContracts.DTOs.Applicant;
using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.Country;
using WorkFinder.ServiceContracts.DTOs.Employer;
using WorkFinder.ServiceContracts.DTOs.Industry;
using WorkFinder.ServiceContracts.DTOs.Job;
using WorkFinder.ServiceContracts.DTOs.Qualification;
using WorkFinder.ServiceContracts.DTOs.Role;
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
            
            //CreateMap<Employer, EmployerResponseDto>()
            //    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
            //    .ForMember(dest => dest.IndustryName, opt => opt.MapFrom(src => src.Industry.IndustryName));

            //Roles
            CreateMap<RolePermission, RolePermissionResponseDto>()
                .ForMember(dest => dest.PermissionId, opt => opt.MapFrom(src => src.Permission.PermissionId))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Permission.Action));

            //Modules
            CreateMap<Module, ModuleResponseDto>();
            CreateMap<Permission, ModuleResponseDto>()
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.Module.ModuleId))
                .ForMember(dest => dest.ModuleName, opt => opt.MapFrom(src => src.Module.ModuleName))
                .ForMember(dest => dest.ParentModuleId, opt => opt.MapFrom(src => src.Module.ParentModuleId))
                .ForMember(dest => dest.Route, opt => opt.MapFrom(src => src.Module.Route));

            //Employers
            CreateMap<EmployerRequestDto, Employer>();
            CreateMap<EmployerRequestDto, RegisterRequestDto>()
                //.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                //.ForMember(dest => dest.ConfirmPassword, opt => opt.MapFrom(src =>src.ConfirmPasswordHash))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName));
            CreateMap<Employer, EmployerResponseDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.User.Role.RoleId))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.User.Role.RoleName))
                .ForMember(dest => dest.IndustryId, opt => opt.MapFrom(src => src.Industry.IndustryId))
                .ForMember(dest => dest.IndustryName, opt => opt.MapFrom(src => src.Industry.IndustryName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.User.Country))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User.IsActive));
            CreateMap<EmployerRequestDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.CompanyName));

            //Applicants
            CreateMap<ApplicantRequestDto, Applicant>();
            CreateMap<ApplicantRequestDto, RegisterRequestDto>();
            CreateMap<SkillResponseDto, ApplicantSkill>();

            //Skills
            CreateMap<SkillRequestDto, Skill>();
            CreateMap<Skill, SkillResponseDto>();
            CreateMap<SkillResponseDto, Skill>();
            CreateMap<string?, Skill>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src));

            //Industries
            CreateMap<IndustryRequestDto, Industry>();
            CreateMap<Industry, IndustryResponseDto>();
            CreateMap<string?, Industry>()
                .ForMember(dest => dest.IndustryName, opt => opt.MapFrom(src => src));

            //Jobs
            CreateMap<JobRequestDto, Job>();
            CreateMap<Job, JobResponseDto>();

            //Qualifications
            CreateMap<Qualification, QualificationResponseDto>();

            //Countries
            CreateMap<Country, CountryResponseDto>();
        }
    }
}
