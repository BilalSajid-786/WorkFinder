using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Job;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for Jobs
    /// </summary>
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IEmployerService _employerService;
        private readonly IMapper _mapper;
        public JobService(IJobRepository jobRepository, IMapper mapper, IEmployerService employerService)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
            _employerService = employerService;
        }

        /// <summary>
        /// Get all jobs from the system
        /// </summary>
        /// <returns>Jobs</returns>
        public async Task<IEnumerable<JobResponseDto>> GetAllJobsAsync()
        {
            var jobs = await _jobRepository.GetAllJobsAsync();
            return _mapper.Map<IEnumerable<JobResponseDto>>(jobs);
        }

        /// <summary>
        /// Get all jobs of an employer
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns>Employer Jobs</returns>
        public async Task<IEnumerable<JobResponseDto>> GetEmployerJobsAsync(Guid employerId)
        {
            var jobs = await _jobRepository.GetEmployerJobsAsync(employerId);
            return _mapper.Map<IEnumerable<JobResponseDto>>(jobs);
        }

        /// <summary>
        /// Insert Job in the system
        /// </summary>
        /// <param name="job"></param>
        /// <returns>Inserted Job</returns>
        public async Task<JobResponseDto> InsertJobAsync(JobRequestDto job)
        {
            if(await _employerService.GetEmployerByIdAsync(job.EmployerId) is not null)
            {
                var insertedJob = await _jobRepository.InsertJobAsync(_mapper.Map<Job>(job));
                return _mapper.Map<JobResponseDto>(insertedJob);
            }
            return new();
        }
    }
}
