using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IMapper _mapper;
        public ApplicantService(IApplicantRepository applicantRepository,IMapper mapper)
        {
            _applicantRepository = applicantRepository;
            _mapper = mapper;
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
    }
}
