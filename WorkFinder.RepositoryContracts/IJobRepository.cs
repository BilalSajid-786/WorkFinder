using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
