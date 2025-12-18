using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Common.Dtos.Pagination;
using WorkFinder.Entities.Entities;
using WorkFinder.ServiceContracts.DTOs.Applicant;
using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.City;
using WorkFinder.ServiceContracts.DTOs.Country;
using WorkFinder.ServiceContracts.DTOs.Employer;
using WorkFinder.ServiceContracts.DTOs.Industry;
using WorkFinder.ServiceContracts.DTOs.Job;
using WorkFinder.ServiceContracts.DTOs.Messages;
using WorkFinder.ServiceContracts.DTOs.Notifications;
using WorkFinder.ServiceContracts.DTOs.Pagination;
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
                .ForMember(dest => dest.Route, opt => opt.MapFrom(src => src.Route));

            //Employers
            CreateMap<EmployerRequestDto, Employer>();
            CreateMap<UpdateEmployerRequestDto, User>();
            CreateMap<UpdateEmployerRequestDto, Employer>();
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
            CreateMap<UpdateApplicantRequestDto, Applicant>();
            CreateMap<UpdateApplicantRequestDto, User>();
            CreateMap<ApplicantRequestDto, RegisterRequestDto>();
            CreateMap<SkillResponseDto, ApplicantSkill>();
            CreateMap<Applicant, ApplicantResponseDto>()
                .ForMember(dest => dest.ApplicantId, opt => opt.MapFrom(src => src.ApplicantId))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Resume, opt => opt.MapFrom(src => src.Resume))
                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.Qualification.QualificationId))
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification.QualificationName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.User.Country))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User.IsActive))
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(s => new SkillResponseDto
                {
                    SkillId = s.Skill.SkillId,
                    SkillName = s.Skill.SkillName
                })));

            //Skills
            CreateMap<SkillRequestDto, Skill>();
            CreateMap<Skill, SkillResponseDto>();
            CreateMap<SkillResponseDto, Skill>();
            CreateMap<ApplicantSkill, Skill>();
            CreateMap<UpdateSkillResponseDto, Skill>();
            CreateMap<string?, Skill>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src));
            CreateMap<JobSkill, string>()
                .ConvertUsing(src => src.SkillName);

            //Industries
            CreateMap<IndustryRequestDto, Industry>();
            CreateMap<Industry, IndustryResponseDto>();
            CreateMap<string?, Industry>()
                .ForMember(dest => dest.IndustryName, opt => opt.MapFrom(src => src));

            //Jobs
            CreateMap<JobRequestDto, Job>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore());
            CreateMap<Job, JobResponseDto>()
                .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.ExpiryDate))
                .ForMember(dest => dest.IndustryName, opt => opt.MapFrom(src => src.Industry.IndustryName))
                .ForMember(dest => dest.IndustryId, opt => opt.MapFrom(src => src.Industry.IndustryId))
                .ForMember(dest => dest.EmployerId, opt => opt.MapFrom(src => src.Employer.EmployerId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Employer.CompanyName))
                .ForMember(dest => dest.PostedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(s => new SkillResponseDto
                {
                    SkillId = s.Skill.SkillId,
                    SkillName = s.Skill.SkillName
                })));
            CreateMap<PaginationRequestDto, Pagination>();
            //CreateMap<Job, JobResponseDto>();
            CreateMap<Job, ApplicantJobsResponseDto>()
                .ForMember(dest => dest.PostedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry.IndustryName))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Employer.CompanyName));

            CreateMap<ApplicantJob, ApplicantJobsResponseDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Job.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Job.Description))
                .ForMember(dest => dest.PostedDate, opt => opt.MapFrom(src => src.Job.CreatedAt))
                .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Job.Industry.IndustryName))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Job.Employer.CompanyName))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Job.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Job.Country))
                .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Job.Skills))
                .ForMember(dest => dest.EmployerId, opt => opt.MapFrom(src => src.Job.EmployerId));

            CreateMap<SavedJob, ApplicantJobsResponseDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Job.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Job.Description))
                .ForMember(dest => dest.PostedDate, opt => opt.MapFrom(src => src.Job.CreatedAt))
                .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Job.Industry.IndustryName))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Job.Employer.CompanyName))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Job.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Job.Country))
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Job.Skills));

            CreateMap<ApplicantApplyJobDto, ApplicantJob>();
            CreateMap<ApplicantApplyJobDto, SavedJob>();

            CreateMap<UpdateJobApplicantStatusRequestDto, ApplicantJob>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ApplicantStatus));

            // Generic PaginatedList mapping
            CreateMap<PaginatedList<Job>, PaginatedList<ApplicantJobsResponseDto>>();
            CreateMap<PaginatedList<Job>, PaginatedList<JobResponseDto>>();
            CreateMap<PaginatedList<ApplicantJob>, PaginatedList<ApplicantJobsResponseDto>>();
            CreateMap<PaginatedList<SavedJob>, PaginatedList<ApplicantJobsResponseDto>>();
            CreateMap<PaginatedList<Applicant>, PaginatedList<ApplicantResponseDto>>();

            //Qualifications
            CreateMap<Qualification, QualificationResponseDto>();

            //Countries
            CreateMap<Country, CountryResponseDto>();

            //Cities
            CreateMap<City, CityResponseDto>();
            //Messages
            CreateMap<MessageRequestDto, Message>();
            CreateMap<Message, MessageResponseDto>();

            //Notifications
            CreateMap<Notification, NotificationResponseDto>();
        }
    }
}
