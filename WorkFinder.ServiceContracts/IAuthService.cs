using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Applicant;
using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.Employer;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Authentication Service
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user by email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Jwt token</returns>
        Task<(string? Token, string? PaymentUrl)> AuthenticateAsync(string email, string password);

        /// <summary>
        /// Authenticates a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Jwt token</returns>
        Task<string> RefreshClaimsAsync(string email);

        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        Task<Guid> RegisterUserAsync(RegisterRequestDto registerRequestDto);

        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <param name="EmployerRequestDto"></param>
        /// <returns></returns>
        Task<EmployerResponseDto> RegisterEmployerAsync(EmployerRequestDto employerRequest);

        /// <summary>
        /// Register an applicant.
        /// </summary>
        /// <param name="applicantRequestDto"></param>
        /// <returns>Applicant Id</returns>
        Task<ApplicantResponseDto> RegisterApplicantAsync(ApplicantRequestDto? applicantRequestDto);
    }
}
