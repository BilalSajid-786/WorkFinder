using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.Employer;
using WorkFinder.ServiceContracts.DTOs.User;

namespace WorkFinder.ServiceContracts
{
    public interface IEmployerService
    {
        /// <summary>
        /// Registers an Employer
        /// </summary>
        /// <returns></returns>
        Task<Guid> RegisterEmployerAsync(EmployerRequestDto employerRequest, Guid userId);

        /// <summary>
        /// Gets all existing employers.
        /// </summary>
        /// <returns>All Employers</returns>
        Task<IEnumerable<EmployerResponseDto>> GetAllEmployers();
    }
}
