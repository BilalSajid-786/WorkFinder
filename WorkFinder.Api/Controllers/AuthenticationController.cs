using Azure;
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
        private readonly ResponseDto _responseDto;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }

        #region Auth

        /// <summary>
        /// Authenticates the user with the given email and password
        /// </summary>
        /// <param name="email">email of the user attempting to login</param>
        /// <returns>Jwt token, if valid</returns>
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                var token = await _authService.AuthenticateAsync(loginRequestDto.Email, loginRequestDto.Password);
                _responseDto.Result= token;
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Token Generation Successfull";
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return Unauthorized(_responseDto);
            }
        }

        #endregion

        #region Registration

        /// <summary>
        /// Registers an employer, if given details are valid
        /// </summary>
        /// <param name="employerRequest"></param>
        /// <returns>ResponseDto</returns>
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> RegisterEmployer(EmployerRequestDto employerRequest)
        {
            EmployerResponseDto? response;
            try
            {
                response = await _authService.RegisterEmployerAsync(employerRequest);
                _responseDto.Result = response;
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Employer Registered Successfully";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        /// <summary>
        /// Registers an Applicant, if given details are valid
        /// </summary>
        /// <param name="applicantRequestDto"></param>
        /// <returns>Applicant Id</returns>
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> RegisterApplicant(ApplicantRequestDto applicantRequestDto)
        {
            ApplicantResponseDto? response;
            try
            {
                response = await _authService.RegisterApplicantAsync(applicantRequestDto);
                _responseDto.Result = response;
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Applicant Registered Successfully";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        #endregion
    }
}
