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
using WorkFinder.ServiceContracts.DTOs.User;

namespace WorkFinder.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IMapper _mapper;
        private readonly PasswordHasher<object> _passwordHasher;
        private readonly IEmployerRepository _employerRepository;
        private readonly IUserRepository _userRepository;
        public EmployerService(IMapper mapper, IEmployerRepository employerRepository, IUserRepository userRepository) 
        {
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<object>();
            _employerRepository = employerRepository;
            _userRepository = userRepository;
        }

        //public async Task<bool> DeleteEmployerAsync(Guid employerId)
        //{
        //    var isDeleted = await _employerRepository.DeleteEmployerAsync(employerId);
        //    if (!isDeleted)
        //    {
        //        throw new Exception($"Employer not found.");
        //    }
        //    return isDeleted;
        //}

        public async Task<string> EditEmployerAsync(Guid employerId, EmployerRequestDto employerRequest)
        {
            var employer = _mapper.Map<Employer>(employerRequest);
            employer.EmployerId = employerId;
            var empStatus = await _employerRepository.EditEmployerAsync(employer);
            if (empStatus == "SUCCESS") {
                var user = _mapper.Map<User>(employerRequest);
                user.Password = _passwordHasher.HashPassword(null, employerRequest.Password);
                var userStatus = await _userRepository.EditUserAsync(user);
                if (userStatus == "SUCCESS")
                {
                    return "Employer updated.";
                }
            }
            return "Employer not updated."; // 0 in case of fail. 1 in case of success.
        }

        public async Task<IEnumerable<EmployerResponseDto>> GetAllEmployers()
        {
            var employers = await _employerRepository.GetAllemployers();
            return _mapper.Map<IEnumerable<EmployerResponseDto>>(employers);
        }

        public async Task<EmployerResponseDto?> GetEmployerByIdAsync(Guid employerId)
        {
            var employer = await _employerRepository.GetEmployerByIdAsync(employerId);
            if (employer == null)
                throw new Exception($"Employer not found.");
            return _mapper.Map<EmployerResponseDto>(employer);
        }

        /// <summary>
        /// Get employerId for a given userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>EmployerId of a user</returns>
        public async Task<Guid?> GetEmployerIdAsync(Guid userId)
        {
            return await _employerRepository.GetEmployerIdAsync(userId);
        }

        public async Task<Guid> RegisterEmployerAsync(EmployerRequestDto employerRequest)
        {
            var employer = _mapper.Map<Employer>(employerRequest);
            return await _employerRepository.RegisterEmployerAsync(employer);
        }

        //public async Task<bool?> UpdateEmployerStatusAsync(Guid userId, bool isActive)
        //{
        //    var updatedStatus = await _employerRepository.UpdateEmployerStatusAsync(userId, isActive);
        //    if(updatedStatus == null)
        //    {
        //        throw new Exception($"Employer not found.");
        //    }
        //    return updatedStatus.Value;
        //}
    }
}
