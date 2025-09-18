using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
