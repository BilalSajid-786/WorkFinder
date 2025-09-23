using Dapper;
using WorkFinder.Entities.Entities;
using WorkFinder.Repositories.DbContext;
using WorkFinder.RepositoryContracts;

namespace WorkFinder.Repositories.Repositories
{
    /// <summary>
    /// Repository Implementation for Job
    /// </summary>
    public class JobRepository : IJobRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public JobRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        /// <summary>
        /// Get all jobs from db.
        /// </summary>
        /// <returns>All jobs</returns>
        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            using var connection = _dapperDbContext.CreateConnection();
            //procedure name
            var sql = "[GetAllJobs]";
            return await connection.QueryAsync<Job>(sql, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get all jobs of a given employer
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns>List of jobs for an employer</returns>
        public async Task<IEnumerable<Job>> GetEmployerJobsAsync(Guid employerId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            //procedure name
            var sql = "[GetEmployerAllJobs]";
            //parameters
            var parameters = new DynamicParameters();
            parameters.Add("@EmployerId", employerId);
            return await connection.QueryAsync<Job>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Insert a job in the db
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task<Job> InsertJobAsync(Job job)
        {
            using var connection = _dapperDbContext.CreateConnection();
            //procedure name
            var sql = "[InsertJob]";
            //parameters
            var parameters = new DynamicParameters();
            parameters.Add("@Title", job.Title);
            parameters.Add("@Description", job.Description);
            parameters.Add("@City", job.City);
            parameters.Add("@ExpiryDate", job.ExpiryDate);
            parameters.Add("@EmployerId", job.EmployerId);
            parameters.Add("@IndustryId", job.IndustryId);
            parameters.Add("@CreatedBy", job.CreatedBy);
            job.JobId = await connection.ExecuteScalarAsync<int>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
            return job;
        }
    }
}
