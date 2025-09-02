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
using WorkFinder.ServiceContracts.DTOs.User;

namespace WorkFinder.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,IRoleService roleService, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var isDeleted = await _userRepository.DeleteUserAsync(userId);
            if (!isDeleted)
            {
                throw new Exception($"User not found.");
            }
            return isDeleted;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return _mapper.Map<UserResponseDto>(user);
        }

        /// <summary>
        /// Gets password hash of a user for given Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Password Hash</returns>
        public async Task<string?> GetUserPasswordHashById(Guid userId)
        {
            return await _userRepository.GetUserPasswordHashById(userId);
        }

        public async Task<Guid> RegisterUserAsync(RegisterRequestDto registerRequestDto,string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(registerRequestDto.Email) || !registerRequestDto.Email.Contains("@"))
                throw new Exception($"{registerRequestDto.Email} is not valid.");

            if (registerRequestDto.Password.Length < 8)
                throw new Exception($"{registerRequestDto.Password} should be greater than or equal to 8");

            if (string.IsNullOrEmpty(registerRequestDto.Email))
                throw new Exception($"Name {registerRequestDto.Name} should not be empty");

            var roles = await _roleService.GetRolesAsync();

            if (!roles.Any(r => r.RoleId == registerRequestDto.RoleId))
                throw new Exception($"RoleId {registerRequestDto.RoleId} doesn't exist in the system");

            var user = new User()
            {
                UserName = registerRequestDto.Name,
                Email = registerRequestDto.Email,
                Password = passwordHash,
                RoleId = registerRequestDto.RoleId,
                City = registerRequestDto.City,
                Country = registerRequestDto.Country,
                Phone = registerRequestDto.Phone,
                CreatedAt = DateTime.UtcNow
            };
            return await _userRepository.RegisterUserAsync(user);
        }

        public async Task<bool?> UpdateUserStatusAsync(Guid userId, bool isActive)
        {
            var updatedStatus = await _userRepository.UpdateUserStatusAsync(userId, isActive);
            if (updatedStatus == null)
            {
                throw new Exception($"User not found.");
            }
            return updatedStatus.Value;
        }
    }
}
