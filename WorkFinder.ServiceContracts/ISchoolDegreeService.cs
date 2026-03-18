using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Qualification;
using WorkFinder.ServiceContracts.DTOs.SchoolDegree;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Service contract for school degrees
    /// </summary>
    public interface ISchoolDegreeService
    {
        /// <summary>
        /// Seed school degrees into system
        /// </summary>
        /// <returns></returns>
        Task SeedSchoolDegreesAsync();

        /// <summary>
        /// Get school degrees from the system
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SchoolDegreeResponseDto>> GetSchoolDegreesAsync();
    }
}
