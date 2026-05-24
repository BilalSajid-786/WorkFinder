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
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IForgotPasswordService _forgotPasswordService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserService _userService;
        private readonly ResponseDto _responseDto;

        public AuthenticationController(IAuthService authService, IForgotPasswordService forgotPasswordService,
            ISubscriptionService subscriptionService, IUserService userService)
        {
            _authService = authService;
            _responseDto = new();
            _forgotPasswordService = forgotPasswordService;
            _subscriptionService = subscriptionService;
            _userService = userService;
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
                var response = await _authService.AuthenticateAsync(loginRequestDto.Email, loginRequestDto.Password);
                if (response.PaymentUrl != null)
                {
                    _responseDto.Result = response.PaymentUrl;
                    _responseDto.IsSuccess = true;
                    _responseDto.Message = "Payment required";
                }
                else
                {
                    _responseDto.Result = response.Token;
                    _responseDto.IsSuccess = true;
                    _responseDto.Message = "Token generation successfull";
                }
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
                //_subscriptionService.CreateCheckoutSubscriptionAsync();
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

        #region ForgotPassword

        /// <summary>
        /// Sent a forgot password link to user email
        /// </summary>
        /// <param name="forgotPasswordDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                await _forgotPasswordService.SendPasswordResetEmail(forgotPasswordDto.Email);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Password reset link sent successfully";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                await _forgotPasswordService.ResetPassword(resetPasswordDto);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        #endregion

        #region Subscription

        [HttpGet("{token}")]
        public async Task<ActionResult<ResponseDto>> ValidateVerificationToken([FromRoute]string token)
        {
            try
            {
                var userDetails = await _userService.GetUserByVerificationToken(Guid.Parse(token));
                _responseDto.Result = userDetails;
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Token is valid";
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
