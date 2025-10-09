using Dapper;
using System.Data;
using WorkFinder.Common.Dtos.Jobs;
using WorkFinder.Common.Dtos.Pagination;
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
        /// Get active jobs from db.
        /// </summary>
        /// <returns>active jobs</returns>

        public async Task<IEnumerable<Job>> GetActveJobsAsync(Pagination pagination)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetActiveJobs]";
            var parameters = new DynamicParameters();
            parameters.Add("@SearchValue", pagination.SearchValue);
            parameters.Add("@SortColumn", pagination.SortColumn);
            parameters.Add("@SortOrder", pagination.SortOrder);
            parameters.Add("@PageSize", pagination.PageSize);
            parameters.Add("@PageNo", pagination.PageNo);
            parameters.Add("@EmployerId", pagination.EmployerId);

            var jobs = (await connection.QueryAsync<Job, Industry, Job>(
                sql,
                (job, industry) =>
                {
                    job.Industry = industry;
                    return job;
                },
                parameters,
                commandType: CommandType.StoredProcedure,
                splitOn: "IndustryId" // tell Dapper where the split happens
            )).ToList();

            if (!jobs.Any())
                return jobs;
            var jobIds = string.Join(",", jobs.Select(j => j.JobId));

            var skillParams = new DynamicParameters();
            skillParams.Add("@JobIds", jobIds);

            var jobSkills = (await connection.QueryAsync<JobSkill, Skill, JobSkill>(
            "[GetJobSkills]",
            (jobSkill, skill) =>
            {
                jobSkill.Skill = skill;
                return jobSkill;
            },
            skillParams,
            commandType: CommandType.StoredProcedure,
            splitOn: "SkillId"
            )).ToList();

            foreach (var job in jobs)
            {
                job.Skills = jobSkills.Where(js => js.JobId == job.JobId).ToList();
            }
            return jobs;
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
        /// Get Available Jobs from db for an applicant
        /// </summary>
        /// <param name="location"></param>
        /// <param name="industryId"></param>
        /// <param name="jobType"></param>
        /// <returns>Available Jobs for an applicant</returns>
        public async Task<PaginatedList<Job>> GetApplicantAvailableJobsAsync(PaginationParameters<AvailableJobsFilter> queryParameters)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetApplicantAvailableJobs]";
            var parameters = new DynamicParameters();
            parameters.Add("@Location", queryParameters.Filters?.Location);
            parameters.Add("@IndustryId", queryParameters.Filters?.IndustryId);
            parameters.Add("@JobType", queryParameters.Filters?.JobType);
            parameters.Add("@SearchValue", queryParameters.SearchValue);
            parameters.Add("@SortColumn", queryParameters.SortColumn);
            parameters.Add("@SortOrder", queryParameters.SortOrder);
            parameters.Add("@PageSize", queryParameters.PageSize);
            parameters.Add("@PageNo", queryParameters.PageNo);

            parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // get paginated jobs
            var jobs = (await connection.QueryAsync<Job, Industry, Employer, Job>(
                "GetApplicantAvailableJobs",
                (job, industry, employer) =>
                {
                    job.Industry = industry;
                    job.Employer = employer;
                    return job;
                },
                param: parameters,
                splitOn: "IndustryId,EmployerId",
                commandType: CommandType.StoredProcedure
            )).ToList();

            var totalCount = parameters.Get<int>("@TotalCount");

            // Combine results
            var paginatedList = new PaginatedList<Job>(
                    jobs.ToList(),
                    parameters.Get<int>("@TotalCount"),
                    queryParameters.PageNo,
                    queryParameters.PageSize
                );

                return paginatedList;
            }

            //var records =await connection.QueryAsync<Job, Industry, Employer,Job>
            //    (sql
            //    ,(job, industry, employer) =>
            //    {
            //        job.Industry = industry;
            //        job.Employer = employer;
            //        return job;
            //    }
            //    , paramters
            //    , splitOn: "IndustryId,EmployerId"
            //    , commandType: System.Data.CommandType.StoredProcedure
            //    );
            //return new PaginatedList<Job>(records, 10, 10, 10);
        

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
            parameters.Add("@Country", job.Country);
            parameters.Add("@JobType", job.JobType);
            parameters.Add("@ExpiryDate", job.ExpiryDate);
            parameters.Add("@EmployerId", job.EmployerId);
            parameters.Add("@IndustryId", job.IndustryId);
            parameters.Add("@CreatedBy", job.CreatedBy);
            job.JobId = await connection.ExecuteScalarAsync<int>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
            return job;
        }

        /// <summary>
        /// Insert the skill for a job in db
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task InsertJobSkill(int skillId, int jobId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[InsertJobSkill]";
            var parameters = new DynamicParameters();
            parameters.Add("@JobId", jobId);
            parameters.Add("@SkillId", skillId);
            await connection.ExecuteAsync(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
