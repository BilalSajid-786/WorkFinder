using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Applicant;
using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.Employer;
using WorkFinder.ServiceContracts.DTOs.Response;

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
        public async Task<ActionResult<ResponseDto>> Login(LoginRequestDto loginRequestDto)
        {
            var token = await _authService.AuthenticateAsync(loginRequestDto.Email, loginRequestDto.Password);
            if (token is null)
                return Unauthorized();

            return new ResponseDto()
            {
                Result = token,
                IsSuccess = true,
                Message = "Token Generation Successfull"
            };
        }

        /// <summary>
        /// Registers a given User in the system, if details are valid
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>Id of the registered user</returns>
        [Authorize(Policy = "Job.Apply")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Register(RegisterRequestDto registerRequest)
        {
            var user = HttpContext.User;
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
        public async Task<ActionResult<ResponseDto>> RegisterApplicant(ApplicantRequestDto applicantRequestDto)
        {
            var response = await _authService.RegisterApplicantAsync(applicantRequestDto);
            return new ResponseDto()
            {
                Result = response,
                IsSuccess = true,
                Message = "Applicant Registered Successfully"
            };
        }
    }
}
