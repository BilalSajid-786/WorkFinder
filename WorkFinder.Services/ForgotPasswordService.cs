using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Authentication;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for Forgot password
    /// </summary>
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IPasswordResetRepository _passwordResetRepository;
        private readonly PasswordHasher<object> _passwordHasher;
        private readonly IConfiguration _configuration;
        public ForgotPasswordService(IUserService userService, IEmailService emailService,
            IPasswordResetRepository passwordResetRepository, IConfiguration configuration)
        {
            _userService = userService;
            _emailService = emailService;
            _passwordResetRepository = passwordResetRepository;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<object>();
        }

        public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            //Validate the token and get the link details
            var passwordResetRequest = await _passwordResetRepository.IsValidToken(resetPasswordDto.Token);
           // Check is token valid or not
            if (passwordResetRequest == null)
                throw new Exception("Invalid token or link");
            if (passwordResetRequest.Used)
                throw new Exception("Link already used");
            if (passwordResetRequest.ExpiresAt < DateTime.UtcNow)
                throw new Exception("Token expired");
            //Create a hash for password
            var passwordHash = _passwordHasher.HashPassword(null, resetPasswordDto.Password);
            await _userService.UpdateUserPassword(passwordHash, passwordResetRequest.UserId);
            await _passwordResetRepository.MarkAsUsed(passwordResetRequest.Id);
        }

        /// <summary>
        /// send a forgot password reset email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task SendPasswordResetEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null)
                return;

            var token = GenerateToken();
            var passwordResetRequest = new PasswordResetRequest()
            {
                UserId = user.UserId,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                Used = false
            };
            await _passwordResetRepository.CreatePasswordResetRequest(passwordResetRequest);

            string? baseUrl = _configuration.GetValue<string>("baseUrl");
            var link = $"{baseUrl}forgot-password?token={token}";
            await _emailService.SendPasswordResetEmail("bilalsajid5432@gmail.com", link);
        }

        private string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                    .Replace("/", "").Replace("+", "").Replace("=", "");
        }
    }
}
