using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs;

namespace WorkFinder.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly PasswordHasher<object> _passwordHasher;
        public AuthService(ITokenService tokenService, IUserService userService)
        {

            _tokenService = tokenService;
            _userService = userService;
            _passwordHasher = new PasswordHasher<object>();
        }
        public async Task<string?> AuthenticateAsync(string email, string password)
        {
            //Get User by email
            var user = await _userService.GetUserByEmailAsync(email);

            //return null if user doesn't exist
            if (user is null)
                return null;
            
            //check passwordHash
            var isValidPassword = _passwordHasher.VerifyHashedPassword(null, user.PasswordHash, password);

            //return token if valid email and password
            if (isValidPassword == PasswordVerificationResult.Success)
                return _tokenService.GenerateToken(user);

            return null;
        }

        public async Task<Guid> RegisterUserAsync(RegisterRequestDto registerRequestDto)
        {
            var passwordHash = _passwordHasher.HashPassword(null, registerRequestDto.Password);
            return await _userService.RegisterUserAsync(registerRequestDto, passwordHash);
        }
    }
}
