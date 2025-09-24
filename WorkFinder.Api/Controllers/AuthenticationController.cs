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


        [HttpPost]
        public async Task<ActionResult<EmployerResponseDto>> RegisterEmployer(EmployerRequestDto employerRequest)
        {
            return await _authService.RegisterEmployerAsync(employerRequest);
        }

        /// <summary>
        /// Registers an Applicant, if given details are valid
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
