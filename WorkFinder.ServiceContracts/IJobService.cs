using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Common.Dtos.Jobs;
using WorkFinder.Common.Dtos.Pagination;
using WorkFinder.ServiceContracts.DTOs.Applicant;
using WorkFinder.ServiceContracts.DTOs.Country;
using WorkFinder.ServiceContracts.DTOs.Job;
using WorkFinder.ServiceContracts.DTOs.Pagination;
using WorkFinder.ServiceContracts.DTOs.Response;
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
        /// Update the given job into db
        /// </summary>
        /// <param name="job"></param>
        /// <returns>Updated Job</returns>
        Task<JobResponseDto> UpdateJobAsync(JobEditRequestDto job);

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
        /// Get a single job by its id.
        /// </summary>
        /// <param name="jobId">Job identifier</param>
        /// <returns>The job if found; otherwise null</returns>
        Task<JobResponseDto?> GetJobByIdAsync(int jobId);

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
        Task<PaginatedList<JobResponseDto>> GetEmployerJobsAsync(PaginationParameters<AvailableJobsFilter> request, Guid employerId);

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

        /// <summary>
        /// Remove saved job for an applicant from the system
        /// </summary>
        /// <param name="applicantSaveJobDto"></param>
        /// <returns></returns>
        Task<bool> UnsaveJobAsync(ApplicantApplyJobDto applicantUnsaveJobDto);

        /// <summary>
        /// Update job status active or inactive
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="status"></param>
        /// <param name="employerId"></param>
        /// <returns></returns>
        Task<int?> UpdateJobStatusAsync(int jobId, bool status, Guid employerId);

        /// <summary>
        /// Delete job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task<int?> DeleteJobAsync(int jobId, Guid employerId);
        /// <summary>
        /// Get Job Applicants By Job Id
        /// </summary>
        /// <param name="jobApplicantRequestDto"></param>
        /// <returns></returns>
        Task<PaginatedList<ApplicantResponseDto>>GetJobApplicantsByIdAsync(PaginationParameters<JobApplicantsFilter> jobApplicantRequestDto);

        /// <summary>
        /// Update Job Applicants Status
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string?> UpdateJobApplicantStatusAsync(UpdateJobApplicantStatusRequestDto request);
    }
}
