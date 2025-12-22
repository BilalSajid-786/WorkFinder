using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Common.Dtos.Applicants;
using WorkFinder.Common.Dtos.Pagination;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Applicant;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for Applicant
    /// </summary>
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<object> _passwordHasher;
        public ApplicantService(IApplicantRepository applicantRepository
            ,IMapper mapper,
            IUserRepository userRepository)
        {
            _applicantRepository = applicantRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<object>();
        }

        /// <summary>
        /// Edit applicant information in the system
        /// </summary>
        /// <param name="applicantRequest"></param>
        /// <returns></returns>
        public async Task<string> UpdateApplicantAsync(UpdateApplicantRequestDto applicantRequest, IAuthService authService)
        {
            var applicant = _mapper.Map<Applicant>(applicantRequest);
            var empStatus = await _applicantRepository.UpdateApplicantAsync(applicant);
            if (empStatus == "SUCCESS")
            {
                var user = _mapper.Map<User>(applicantRequest);
                if (user.Password.Length > 0)
                    user.Password = _passwordHasher.HashPassword(null, applicantRequest.Password);
                var userStatus = await _userRepository.EditUserAsync(user);
                if (userStatus == "SUCCESS")
                {
                    var skills = await _applicantRepository.GetApplicantSkillsAsync(applicant.ApplicantId);
                    var savedSkills = skills.Select(s => s.SkillId).ToList();
                    var receivedSkills = applicant.Skills.Select(s => s.SkillId).ToList();

                    var newSkills = receivedSkills
                   .Where(s => !savedSkills.Contains(s))
                   .Select(s => new Skill { SkillId = s });

                    var removedSkills = savedSkills
                        .Where(s => !receivedSkills.Contains(s))
                        .Select(s => new Skill { SkillId = s });

                    foreach (var skill in newSkills)
                    {
                        await _applicantRepository.AddApplicantSkillAsync(skill,applicant.ApplicantId);
                    }

                    foreach (var skill in removedSkills)
                    {
                        await _applicantRepository.RemoveSkillAsync(skill, applicant.ApplicantId);
                    }

                    return await authService.RefreshClaimsAsync(user.Email);
                }
            }
            return "Applicant not updated."; // 0 
        }

        /// <summary>
        /// Get ApplicantId from the system
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>ApplicantId</returns>
        public async Task<Guid?> GetApplicantIdAsync(Guid userId)
        {
            return await _applicantRepository.GetApplicantIdAsync(userId);
        }

        /// <summary>
        /// Get applicants
        /// </summary>
        /// <param name="applicantRequestDto"></param>
        /// <returns>Applicants</returns>
        public async Task<PaginatedList<ApplicantResponseDto>> GetApplicantsAsync(PaginationParameters<ApplicantsFilter> applicantRequestDto)
        {
            var applicants = await _applicantRepository.GetApplicantsAsync(applicantRequestDto);
            return _mapper.Map<PaginatedList<ApplicantResponseDto>>(applicants);
        }

        /// <summary>
        /// Insert an applicant into the system
        /// </summary>
        /// <param name="applicantRequestDto"></param>
        /// <returns>ApplicantId</returns>
        public async Task<Guid> InsertApplicantAsync(ApplicantRequestDto applicantRequestDto)
        {

            var applicantId = await _applicantRepository.InsertApplicantAsync(_mapper.Map<Applicant>(applicantRequestDto));
            
            //Skill Insertion for applicant
            foreach (var skill in applicantRequestDto.Skills)
            {

                await _applicantRepository.AddApplicantSkillAsync(_mapper.Map<Skill>(skill), applicantId);
            }

            return applicantId;
        }

        /// <summary>
        /// Check, is Applicant exist in the system
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        public async Task<bool> IsApplicantExistAsync(Guid applicantId)
        {
            return await _applicantRepository.IsApplicantExistAsync(applicantId);
        }

        /// <summary>
        /// Updates a resume for an applicant in the system
        /// </summary>
        /// <returns></returns>
        public async Task UpdateApplicantResume(string resumeName, Guid applicantId)
        {
            await _applicantRepository.UpdateApplicantResume(resumeName, applicantId);
        }

        /// <summary>
        /// Gets an applicant by id
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        public async Task<ApplicantResponseDto> GetApplicantByIdAsync(Guid applicantId)
        {
            var applicant = await _applicantRepository.GetApplicantByIdAsync(applicantId);
            var applicantResponse = _mapper.Map<ApplicantResponseDto>(applicant);
            applicantResponse.QualificationId = applicant.QualificationId;
            return applicantResponse;
        }
    }
}
