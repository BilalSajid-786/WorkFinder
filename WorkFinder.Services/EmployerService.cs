using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Employer;

namespace WorkFinder.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IMapper _mapper;
        private readonly IEmployerRepository _employerRepository;
        public EmployerService(IMapper mapper, IEmployerRepository employerRepository) 
        {
            _mapper = mapper;
            _employerRepository = employerRepository;
        }

        public async Task<Guid> RegisterEmployerAsync(EmployerRequestDto employerRequest, Guid userId)
        {
            var employer = _mapper.Map<Employer>(employerRequest);
            employer.UserId = userId;
            //Employer employer = new Employer();
            return await _employerRepository.RegisterEmployerAsync(employer);
        }
    }
}
