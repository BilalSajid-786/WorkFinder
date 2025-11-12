using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Common.Dtos.Applicants;
using WorkFinder.Common.Dtos.Jobs;
using WorkFinder.Common.Dtos.Pagination;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contract for Applicant
    /// </summary>
    public interface IApplicantRepository
    {
        /// <summary>
        /// Insert the applicant into the database
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns>Applicant Id</returns>
        Task<Guid> InsertApplicantAsync(Applicant applicant);

        /// <summary>
        /// Add a skill for applicant
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="ApplicantId"></param>
        Task AddApplicantSkillAsync(Skill skill, Guid applicantId);

        /// <summary>
        /// Check is applicant exists in the system
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        Task<bool> IsApplicantExistAsync(Guid applicantId);

        /// <summary>
        /// Get applicantId against userId from the db
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>ApplicantId</returns>
        Task<Guid?> GetApplicantIdAsync(Guid userId);

        /// <summary>
        /// Updates a resume for an applicant
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        Task UpdateApplicantResume(string resumeName, Guid applicantId);

        /// <summary>
        /// Get Applicants
        /// </summary>
        /// <param name="applicantRequestDto"></param>
        /// <returns></returns>
        Task<PaginatedList<Applicant>> GetApplicantsAsync(PaginationParameters<ApplicantsFilter> applicantRequestDto);
    }
}
