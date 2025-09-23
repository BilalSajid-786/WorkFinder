using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Applicant;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Service Contract for Applicant
    /// </summary>
    public interface IApplicantService
    {
        /// <summary>
        /// Inserts the applicant in to the system
        /// </summary>
        /// <param name="applicantRequestDto"></param>
        /// <returns>Applicant Id</returns>
        Task<Guid> InsertApplicantAsync(ApplicantRequestDto applicantRequestDto);

        /// <summary>
        /// Check if applicant exists in the system
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        Task<bool> IsApplicantExistAsync(Guid applicantId);

        /// <summary>
        /// Get applicantId against userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>ApplicantId</returns>
        Task<Guid?> GetApplicantIdAsync(Guid userId);
    }
}
