using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Applicant;
using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.Employer;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates the user with the given email and password
        /// </summary>
        /// <param name="email">email of the user attempting to login</param>
        /// <returns>Jwt token, if valid</returns>
        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginRequestDto loginRequestDto)
        {
            var token = await _authService.AuthenticateAsync(loginRequestDto.Email, loginRequestDto.Password);
            return Ok(new { token = token });
        }

        /// <summary>
        /// Registers a given User in the system, if details are valid
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>Id of the registered user</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> Register(RegisterRequestDto registerRequest)
        {
            return await _authService.RegisterUserAsync(registerRequest);
        }
        [HttpPost]
        public async Task<ActionResult<EmployerResponseDto>> RegisterEmployer(EmployerRequestDto employerRequest)
        {
            return await _authService.RegisterEmployerAsync(employerRequest);
        }

        /// <summary>
        /// Registers an Applicant
        /// </summary>
        /// <param name="applicantRequestDto"></param>
        /// <returns>Applicant Id</returns>
        [HttpPost]
        public async Task<ActionResult<ApplicantResponseDto>> RegisterApplicant(ApplicantRequestDto applicantRequestDto)
        {
            return await _authService.RegisterApplicantAsync(applicantRequestDto);
        }
    }
}
