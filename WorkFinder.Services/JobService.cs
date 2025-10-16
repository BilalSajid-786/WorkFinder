using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Common.Dtos.Jobs;
using WorkFinder.Common.Dtos.Pagination;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Job;
using WorkFinder.ServiceContracts.DTOs.Pagination;
using WorkFinder.ServiceContracts.Enums;

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
        /// Insert Applicant application for a job
        /// </summary>
        /// <param name="applicantApplyJobDto"></param>
        /// <returns></returns>
        public async Task<bool> ApplyJobAsync(ApplicantApplyJobDto applicantApplyJobDto)
        {
            var applicantJob = _mapper.Map<ApplicantJob>(applicantApplyJobDto);
            applicantJob.Status = StatusType.Applied.ToString();
            return await _jobRepository.ApplyJobAsync(applicantJob);
        }

        /// <summary>
        /// Get active jobs from the system
        /// </summary>
        /// <returns>Employer Jobs</returns>
        public async Task<IEnumerable<JobResponseDto>> GetEmployerJobsAsync(PaginationRequestDto paginationRequestDto, Guid employerId)
        {
            var pagination = _mapper.Map<Pagination>(paginationRequestDto);
            var activeJobs = await _jobRepository.GetEmployerJobsAsync(pagination, employerId);
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
        /// Get applicant applied jobs
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PaginatedList<ApplicantJobsResponseDto>> GetApplicantAppliedJobsAsync(PaginationParameters<AvailableJobsFilter> request)
        {
            var jobs = await _jobRepository.GetApplicantAppliedJobsAsync(request);
            return _mapper.Map<PaginatedList<ApplicantJobsResponseDto>>(jobs);
        }

        public async Task<PaginatedList<ApplicantJobsResponseDto>> GetApplicantAvailableJobsAsync(PaginationParameters<AvailableJobsFilter> request)
        {
            var jobs = await _jobRepository.GetApplicantAvailableJobsAsync(request);
            return _mapper.Map<PaginatedList<ApplicantJobsResponseDto>>(jobs);
        }

        /// <summary>
        /// Get saved jobs for an applicant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PaginatedList<ApplicantJobsResponseDto>> GetApplicantSavedJobsAsync(PaginationParameters<AvailableJobsFilter> request)
        {
            var jobs = await _jobRepository.GetApplicantSavedJobsAsync(request);
            return _mapper.Map<PaginatedList<ApplicantJobsResponseDto>>(jobs);
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

        /// <summary>
        /// Save job for an applicant
        /// </summary>
        /// <param name="applicantSaveJobDto"></param>
        /// <returns></returns>
        public async Task<bool> SaveJobAsync(ApplicantApplyJobDto applicantSaveJobDto)
        {
            return await _jobRepository.SaveJobAsync(_mapper.Map<SavedJob>(applicantSaveJobDto));
        }

        public async Task<int?> UpdateJobStatusAsync(int jobId, bool status, Guid employerId)
        {
            return await _jobRepository.UpdateJobStatusAsync(jobId, status, employerId);
        }
    }
}
