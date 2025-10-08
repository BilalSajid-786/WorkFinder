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
using WorkFinder.ServiceContracts.DTOs.Pagination;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for Jobs
    /// </summary>
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IEmployerService _employerService;
        private readonly IIndustryService _industryService;
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;
        public JobService(IJobRepository jobRepository, IMapper mapper, IEmployerService employerService, 
            IIndustryService industryService, ISkillService skillService)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
            _employerService = employerService;
            _industryService = industryService;
            _skillService = skillService;
        }

        /// <summary>
        /// Get active jobs from the system
        /// </summary>
        /// <returns>Active Jobs</returns>
        public async Task<IEnumerable<JobResponseDto>> GetActiveJobsAsync(PaginationRequestDto paginationRequestDto)
        {
            var pagination = _mapper.Map<Pagination>(paginationRequestDto);
            var activeJobs = await _jobRepository.GetActveJobsAsync(pagination);
            return _mapper.Map<IEnumerable<JobResponseDto>>(activeJobs);
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
            if (await _employerService.GetEmployerByIdAsync(job.EmployerId) is null)
                throw new Exception($"Invalid Employer Id {job.EmployerId}");
            if (await _industryService.GetIndustryByIdAsync(job.IndustryId) is null)
                throw new Exception($"Invalid Industry Id {job.IndustryId}");

            var insertedJob = await _jobRepository.InsertJobAsync(_mapper.Map<Job>(job));
            if(insertedJob.JobId > 0)
            {
                if(job.Skills is not null)
                {
                    int skillId = 0;
                    foreach (var skill in job.Skills)
                    {
                        if(skill.SkillId == 0)
                        {
                            skillId = await _skillService.InsertSkill(new()
                            {
                                SkillName = skill.SkillName
                            });
                        }
                        else
                        {
                            skillId = skill.SkillId;
                        }
                        await _jobRepository.InsertJobSkill(skillId, insertedJob.JobId);

                    }
                }
            }
            return _mapper.Map<JobResponseDto>(insertedJob);
        }
    }
}
