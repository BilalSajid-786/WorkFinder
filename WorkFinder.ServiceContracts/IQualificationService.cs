using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Qualification;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Service contract for Qualification
    /// </summary>
    public interface IQualificationService
    {
        /// <summary>
        /// Seed qualifications into system
        /// </summary>
        /// <returns></returns>
        Task SeedQualficationAsync();

        /// <summary>
        /// Get qualifications from the system
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<QualificationResponseDto>> GetQualificationsAsync();
    }
}
