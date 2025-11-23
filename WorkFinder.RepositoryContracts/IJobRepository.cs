using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Common.Dtos.Jobs;
using WorkFinder.Common.Dtos.Pagination;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository contract for Jobs
    /// </summary>
    public interface IJobRepository
    {
        /// <summary>
        /// Insert the given job into db
        /// </summary>
        /// <param name="job"></param>
        /// <returns>Newly inserted Job</returns>
        Task<Job> InsertJobAsync(Job job);

        /// <summary>
        /// Insert the skill for job
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task InsertJobSkill(int skillId, int jobId);

        /// <summary>
        /// Get all jobs from the db
        /// </summary>
        /// <returns>All jobs</returns>
        Task<IEnumerable<Job>> GetAllJobsAsync();

        /// <summary>
        /// Get all jobs of an employer
        /// </summary>
        /// <returns>All jobs of a specific employer</returns>
        Task<IEnumerable<Job>> GetEmployerJobsAsync(Guid employerId);

        /// <summary>
        /// Get available jobs for an applicant
        /// </summary>
        /// <param name="location"></param>
        /// <param name="industryId"></param>
        /// <param name="jobType"></param>
        /// <returns>Available jobs according to filter values</returns>
        Task<PaginatedList<Job>> GetApplicantAvailableJobsAsync(PaginationParameters<AvailableJobsFilter> queryParameters);

        /// <summary>
        /// Get employer jobs from the db
        /// </summary>
        /// <returns>Employer jobs</returns>
        Task<PaginatedList<Job>> GetEmployerJobsAsync(PaginationParameters<AvailableJobsFilter> queryParameters, Guid employerId);

        /// <summary>
        /// Get applied jobs for an applicant
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        Task<PaginatedList<ApplicantJob>> GetApplicantAppliedJobsAsync(PaginationParameters<AvailableJobsFilter> queryParameters);

        /// <summary>
        /// Get saved jobs for an applicant
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        Task<PaginatedList<SavedJob>> GetApplicantSavedJobsAsync(PaginationParameters<AvailableJobsFilter> queryParameters);



        /// <summary>
        /// Get active jobs from the db
        /// Update employer jobs status from the db
        /// </summary>
        /// <returns>Active jobs</returns>
        //Task<IEnumerable<Job>> GetActveJobsAsync(Pagination pagination);

        /// <summary>
        /// Insert an applicant application for a job
        /// </summary>
        /// <param name="applicantJob"></param>
        /// <returns></returns>
        Task<bool> ApplyJobAsync(ApplicantJob applicantJob);

        /// <summary>
        /// Update employer jobs status from the db
        /// </summary>
        /// <returns>Update jobs</returns>
        Task<int?> UpdateJobStatusAsync(int jobId, bool status, Guid employerId);

        /// <summary>
        /// Insert a saved job for an applicant
        /// </summary>
        /// <param name="savedJob"></param>
        /// <returns></returns>
        Task<bool> SaveJobAsync(SavedJob savedJob);

        /// <summary>
        /// Removes a saved job for an applicant
        /// </summary>
        /// <param name="unsavedJob"></param>
        /// <returns></returns>
        Task<bool> UnsaveJobAsync(SavedJob unsavedJob);

        /// <summary>
        /// Get Job Applicants by Job Id
        /// </summary>
        /// <param name="jobApplicantRequestDto"></param>
        /// <returns></returns>
        Task<PaginatedList<Applicant>>GetJobApplicantsByIdAsync(PaginationParameters<JobApplicantsFilter> jobApplicantRequestDto);

        /// <summary>
        /// Update Job Applicants Status
        /// </summary>
        /// <param name="applicantJob"></param>
        /// <returns></returns>
        Task<string?> UpdateJobApplicantStatusAsync(ApplicantJob applicantJob);
    }
}
