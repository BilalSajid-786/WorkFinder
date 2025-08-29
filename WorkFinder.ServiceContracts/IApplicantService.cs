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
    }
}
