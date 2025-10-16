using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Common.Dtos.Jobs;
using WorkFinder.Common.Dtos.Pagination;
using WorkFinder.ServiceContracts.DTOs.Country;
using WorkFinder.ServiceContracts.DTOs.Job;
using WorkFinder.ServiceContracts.DTOs.Pagination;
using WorkFinder.ServiceContracts.DTOs.Skill;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Service Contract for Jobs
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// Insert the given job into db
        /// </summary>
        /// <param name="job"></param>
        /// <returns>Newly inserted Job</returns>
        Task<JobResponseDto> InsertJobAsync(JobRequestDto job);

        /// <summary>
        /// Get all jobs from the db
        /// </summary>
        /// <returns>All jobs</returns>
        Task<IEnumerable<JobResponseDto>> GetAllJobsAsync();

        /// <summary>
        /// Get all jobs of an employer
        /// </summary>
        /// <returns>All jobs of a specific employer</returns>
        Task<IEnumerable<JobResponseDto>> GetEmployerJobsAsync(Guid employerId);

        /// <summary>
        /// Get available jobs for an applicant
        /// </summary>
        /// <param name="applicantJobRequestDto"></param>
        /// <returns>Available Jobs for an applicant</returns>
        Task<PaginatedList<ApplicantJobsResponseDto>> GetApplicantAvailableJobsAsync(PaginationParameters<AvailableJobsFilter> request);

        /// <summary>
        /// Get employer jobs from system
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JobResponseDto>> GetEmployerJobsAsync(PaginationRequestDto paginationRequestDto, Guid employerId);

        /// <summary>
        /// Get applied Jobs for an applicant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginatedList<ApplicantJobsResponseDto>> GetApplicantAppliedJobsAsync(PaginationParameters<AvailableJobsFilter> request);

        /// <summary>
        /// Get saved Jobs for an applicant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginatedList<ApplicantJobsResponseDto>> GetApplicantSavedJobsAsync(PaginationParameters<AvailableJobsFilter> request);

        /// <summary>
        /// Get active jobs from system
        /// </summary>
        /// <returns></returns>
        //Task<IEnumerable<JobResponseDto>> GetActiveJobsAsync(PaginationRequestDto paginationRequestDto);

        /// <summary>
        /// Insert Applicant Application for a job
        /// </summary>
        /// <param name="applicantApplyJobDto"></param>
        /// <returns></returns>
        Task<bool> ApplyJobAsync(ApplicantApplyJobDto applicantApplyJobDto);

        /// <summary>
        /// Insert Applicant Save Job
        /// </summary>
        /// <param name="applicantSaveJobDto"></param>
        /// <returns></returns>
        Task<bool> SaveJobAsync(ApplicantApplyJobDto applicantSaveJobDto);
        Task<int?> UpdateJobStatusAsync(int jobId, bool status, Guid employerId);
    }
}
