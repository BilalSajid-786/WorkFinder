using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.Employer;

namespace WorkFinder.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly PasswordHasher<object> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IEmployerService _employerService;
        public AuthService(ITokenService tokenService, IUserService userService, IMapper mapper, IEmployerService employerService)
        {

            _tokenService = tokenService;
            _userService = userService;
            _passwordHasher = new PasswordHasher<object>();
            _mapper = mapper;
            _employerService = employerService;
        }
        public async Task<string?> AuthenticateAsync(string email, string password)
        {
            //Get User by email
            var user = await _userService.GetUserByEmailAsync(email);

            //return null if user doesn't exist
            if (user is null)
                return null;

            var passwordHash = await _userService.GetUserPasswordHashById(user.UserId);

            if(passwordHash is null) 
                return null;
            
            //check passwordHash
            var isValidPassword = _passwordHasher.VerifyHashedPassword(null, passwordHash, password);

            //return token if valid email and password
            if (isValidPassword == PasswordVerificationResult.Success)
                return _tokenService.GenerateToken(user);

            return null;
        }

        public async Task<EmployerResponseDto> RegisterEmployerAsync(EmployerRequestDto employerRequest)
        {
            var registerRequestDto = _mapper.Map<RegisterRequestDto>(employerRequest);
            var passwordHash = _passwordHasher.HashPassword(null, registerRequestDto.Password);
            //return await _userService.RegisterUserAsync(registerRequestDto, passwordHash);
            var userId = await _userService.RegisterUserAsync(registerRequestDto, passwordHash);
            if (userId == Guid.Empty)
                throw new InvalidOperationException("Failed to register user.");

            var employerId = await _employerService.RegisterEmployerAsync(employerRequest, userId);
            if (employerId == Guid.Empty)
                throw new InvalidOperationException("Failed to register employer.");

            return new EmployerResponseDto
            {
                UserId = userId,
                EmployerId = employerId
            };
        }

        public async Task<Guid> RegisterUserAsync(RegisterRequestDto registerRequestDto)
        {
            var passwordHash = _passwordHasher.HashPassword(null, registerRequestDto.Password);
            return await _userService.RegisterUserAsync(registerRequestDto, passwordHash);
        }
    }
}
