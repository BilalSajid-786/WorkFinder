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
        public EmployerService(IMapper mapper, IEmployerRepository employerRepository) 
        {
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<object>();
            _employerRepository = employerRepository;
        }

        public async Task<bool> DeleteEmployerAsync(Guid userId)
        {
            var isDeleted = await _employerRepository.DeleteEmployerAsync(userId);
            if (!isDeleted)
            {
                throw new Exception($"Employer not found.");
            }
            return isDeleted;
        }

        public async Task<int> EditEmployerAsync(Guid userId, EmployerRequestDto employerRequest)
        {
            var employer = _mapper.Map<Employer>(employerRequest);
            employer.Password = _passwordHasher.HashPassword(null, employer.Password);
            var rowsAffected = await _employerRepository.EditEmployerAsync(userId, employer);
            return rowsAffected; // 0 in case of fail. 1 in case of success.
        }

        public async Task<IEnumerable<EmployerResponseDto>> GetAllEmployers()
        {
            var employers = await _employerRepository.GetAllemployers();
            return _mapper.Map<IEnumerable<EmployerResponseDto>>(employers);
        }

        public async Task<EmployerResponseDto?> GetEmployerByIdAsync(Guid userId)
        {
            var employer = await _employerRepository.GetEmployerByIdAsync(userId);
            if (employer == null)
                throw new Exception($"Employer not found.");
            return _mapper.Map<EmployerResponseDto>(employer);
        }

        public async Task<Guid> RegisterEmployerAsync(EmployerRequestDto employerRequest, Guid userId)
        {
            var employer = _mapper.Map<Employer>(employerRequest);
            employer.UserId = userId;
            //Employer employer = new Employer();
            return await _employerRepository.RegisterEmployerAsync(employer);
        }

        public async Task<bool?> UpdateEmployerStatusAsync(Guid userId, bool isActive)
        {
            var updatedStatus = await _employerRepository.UpdateEmployerStatusAsync(userId, isActive);
            if(updatedStatus == null)
            {
                throw new Exception($"Employer not found.");
            }
            return updatedStatus.Value;
        }
    }
}
