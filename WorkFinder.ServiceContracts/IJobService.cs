using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Job;
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
    }
}
