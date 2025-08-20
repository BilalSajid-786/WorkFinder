using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs;

namespace WorkFinder.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<Guid> RegisterUserAsync(RegisterRequestDto registerRequestDto,string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(registerRequestDto.Email) || !registerRequestDto.Email.Contains("@"))
                throw new Exception($"{registerRequestDto.Email} is not valid.");

            if (registerRequestDto.Password.Length < 8)
                throw new Exception($"{registerRequestDto.Password} should be greater than or equal to 8");

            if (string.IsNullOrEmpty(registerRequestDto.Email))
                throw new Exception($"Name {registerRequestDto.Name} should not be empty");

            var user = new User()
            {
                Name = registerRequestDto.Name,
                Email = registerRequestDto.Email,
                PasswordHash = passwordHash,
            };
            return await _userRepository.RegisterUserAsync(user);
        }
    }
}
