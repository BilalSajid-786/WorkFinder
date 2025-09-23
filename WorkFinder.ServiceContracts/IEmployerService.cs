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
        Task<Guid> RegisterEmployerAsync(EmployerRequestDto employerRequest);

        /// <summary>
        /// Gets all existing employers.
        /// </summary>
        /// <returns>All Employers</returns>
        Task<IEnumerable<EmployerResponseDto>> GetAllEmployers();

        Task<string> EditEmployerAsync(Guid employerId, EmployerRequestDto employerRequest);
        Task<EmployerResponseDto?> GetEmployerByIdAsync(Guid employerId);

        /// <summary>
        /// Gets an employerId for a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>EmployerId</returns>
        Task<Guid?> GetEmployerIdAsync(Guid userId);


    }
}
