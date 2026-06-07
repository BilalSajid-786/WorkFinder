using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public UserService(IUserRepository userRepository, IRoleService roleService
            ,IMapper mapper)
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

        private IEnumerable<ParentModuleResponseDto> MapSideBarItemByRoles(IEnumerable<ModuleResponseDto> modules)
        {
            // Modules having their parent modules existing
            var moduleWithParents = modules.Where(m => m.PermissionId == 0 && m.ParentModuleId == null)
                .Select(i => new ParentModuleResponseDto
                {
                    ParentModuleId = i.ModuleId,
                    ParentModuleName = i.ModuleName,
                    SubModules = modules.Where(s => s.ParentModuleId != null && s.ParentModuleId == i.ModuleId)
                    .GroupBy(s => s.ModuleId)
                    .Select(m => new ModuleResponseDto
                    {
                        ModuleId = m.Key,
                        ModuleName = m.First().ModuleName,
                        Route = m.First().Route,
                    }).ToList()
                });

            //Modules that don't have any parentModule
            var moduleWithOutParents = modules.Where(s => s.PermissionId != 0 && s.ParentModuleId == null)
                .GroupBy(s => s.ModuleId)
                .Select(g => new ParentModuleResponseDto
                {
                    ParentModuleId = g.Key,
                    ParentModuleName = g.First().ModuleName,
                    SubModules = g.Select(m => new ModuleResponseDto
                    {
                        ModuleId = g.Key,
                        ModuleName = m.DisplayName,
                    }).ToList(),
                });

            //Concat both modules
            return moduleWithParents.Concat(moduleWithOutParents);
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

        public async Task<Guid> RegisterUserAsync(RegisterRequestDto registerRequestDto, string passwordHash)
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

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task UpdateUserPassword(string password, Guid userId)
        {
            await _userRepository.UpdateUserPassword(password, userId);
        }

        /// <summary>
        /// Update User profile pic name in the system
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="profilePicName"></param>
        /// <returns></returns>
        public async Task UpdateUserProfilePic(Guid userId, string profilePicName)
        {
            await _userRepository.UpdateUserProfilePic(userId, profilePicName);
        }

        public async Task<string> GetUserStripeId(Guid userId)
        {
            return await _userRepository.GetUserStripeId(userId);
        }

        public async Task InsertUserVerificationToken(Guid userId, Guid verificationToken)
        {
            await _userRepository.InsertUserVerificationToken(userId, verificationToken);
        }

        public async Task<Guid> GetUserVerificationToken(Guid userId)
        {
            return await _userRepository.GetUserVerificationToken(userId);
        }

        public async Task<UserResponseDto?> GetUserByVerificationToken(Guid verificationToken)
        {
            var user = await _userRepository.GetUserByVerificationToken(verificationToken);
            var userSubDetails = await GetUserByEmailAsync(user.Email);
            userSubDetails.AccessStatus = userSubDetails?.AccessStatus ?? null;
            return userSubDetails;
            //var userResponse = _mapper.Map<UserResponseDto>(user);
            //userResponse.AccessStatus = userSubDetails?.AccessStatus ?? null;
            //return userResponse;
        }

        /// <summary>
        /// Check if email already exists for other users
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsEmailExistForOtherUser(Guid userId, string email)
        {
            return await _userRepository.IsEmailExistForOtherUser(userId, email);
        }
    }
}
