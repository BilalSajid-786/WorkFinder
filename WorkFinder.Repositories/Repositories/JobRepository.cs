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
        /// Get employer jobs from db.
        /// </summary>
        /// <returns>employer jobs</returns>

        public async Task<IEnumerable<Job>> GetEmployerJobsAsync(Pagination pagination, Guid employerId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetEmployerJobs]";
            var parameters = new DynamicParameters();
            parameters.Add("@SearchValue", pagination.SearchValue);
            parameters.Add("@SortColumn", pagination.SortColumn);
            parameters.Add("@SortOrder", pagination.SortOrder);
            parameters.Add("@PageSize", pagination.PageSize);
            parameters.Add("@PageNo", pagination.PageNo);
            parameters.Add("@Status", pagination.Status);
            parameters.Add("@EmployerId", employerId);

            var jobs = (await connection.QueryAsync<Job, Employer, Industry, Job>(
                sql,
                (job, employer, industry) =>
                {
                    job.Employer = employer;
                    job.Industry = industry;
                    return job;
                },
                parameters,
                commandType: CommandType.StoredProcedure,
                splitOn: "EmpSplit,IndSplit" // tell Dapper where the split happens
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


        async Task<IEnumerable<JobSkill>> GetJobSkillsAsync(string jobIds)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetJobSkills]";
            var parameters = new DynamicParameters();
            parameters.Add("@JobIds", jobIds);
            return await connection.QueryAsync<JobSkill, Skill, JobSkill>(
            sql,
            (jobSkill, skill) =>
            {
                jobSkill.SkillId = skill.SkillId;
                jobSkill.SkillName = skill.SkillName;
                return jobSkill;
            },
            parameters,
            commandType: CommandType.StoredProcedure,
            splitOn: "SkillId"
            );
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
            parameters.Add("@ApplicantId", queryParameters.Filters?.ApplicantId);
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
                    job.EmployerId = employer.EmployerId;
                    job.Industry = industry;
                    job.Employer = employer;
                    return job;
                },
                param: parameters,
                splitOn: "IndustryId,EmployerId",
                commandType: CommandType.StoredProcedure
            )).ToList();

            if (jobs.Any())
            {
                var jobIds = string.Join(",", jobs.Select(j => j.JobId));
                var jobSkills = await GetJobSkillsAsync(jobIds);
                foreach (var job in jobs)
                {
                    job.Skills = jobSkills.Where(js => js.JobId == job.JobId);
                }
            }

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

        /// <summary>
        /// insert applicant application for a job
        /// </summary>
        /// <param name="applicantJob"></param>
        /// <returns></returns>
        public async Task<bool> ApplyJobAsync(ApplicantJob applicantJob)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[ApplyJob]";
            var parameters = new DynamicParameters();
            parameters.Add("@JobId", applicantJob.JobId);
            parameters.Add("@ApplicantId", applicantJob.ApplicantId);
            parameters.Add("@Status", applicantJob.Status);
            return await connection.ExecuteScalarAsync<bool>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get applicant applied jobs
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PaginatedList<ApplicantJob>> GetApplicantAppliedJobsAsync(PaginationParameters<AvailableJobsFilter> queryParameters)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetApplicantAppliedJobs]";
            var parameters = new DynamicParameters();
            parameters.Add("@ApplicantId", queryParameters.Filters?.ApplicantId);
            parameters.Add("@SearchValue", queryParameters.SearchValue);
            parameters.Add("@SortColumn", queryParameters.SortColumn);
            parameters.Add("@SortOrder", queryParameters.SortOrder);
            parameters.Add("@PageSize", queryParameters.PageSize);
            parameters.Add("@PageNo", queryParameters.PageNo);

            parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // get paginated jobs
            var jobs = (await connection.QueryAsync<Job, Industry, Employer,ApplicantJob, ApplicantJob>(
                sql,
                (job, industry, employer,applicantJob) =>
                {
                    job.Industry = industry;
                    job.Employer = employer;
                    job.EmployerId = employer.EmployerId;
                    applicantJob.Job = job;
                    applicantJob.JobId = job.JobId;
                    return applicantJob;
                },
                param: parameters,
                splitOn: "IndustryId,EmployerId,Status",
                commandType: CommandType.StoredProcedure
            )).ToList();

            if (jobs.Any())
            {
                var jobIds = string.Join(",", jobs.Select(j => j.JobId));
                var jobSkills = await GetJobSkillsAsync(jobIds);
                foreach (var job in jobs)
                {
                    job.Job.Skills = jobSkills.Where(js => js.JobId == job.JobId);
                }
            }

            var totalCount = parameters.Get<int>("@TotalCount");

            // Combine results
            var paginatedList = new PaginatedList<ApplicantJob>(
                    jobs,
                    parameters.Get<int>("@TotalCount"),
                    queryParameters.PageNo,
                    queryParameters.PageSize
                );

            return paginatedList;
        }

        /// <summary>
        /// Insert a saved job for an applicant
        /// </summary>
        /// <param name="savedJob"></param>
        /// <returns></returns>
        public async Task<bool> SaveJobAsync(SavedJob savedJob)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[SaveJob]";
            var parameters = new DynamicParameters();
            parameters.Add("@ApplicantId", savedJob.ApplicantId);
            parameters.Add("@JobId",savedJob.JobId);
            return await connection.ExecuteScalarAsync<bool>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get saved jobs for an applicant
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public async Task<PaginatedList<SavedJob>> GetApplicantSavedJobsAsync(PaginationParameters<AvailableJobsFilter> queryParameters)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetApplicantSavedJobs]";
            var parameters = new DynamicParameters();
            parameters.Add("@ApplicantId", queryParameters.Filters?.ApplicantId);
            parameters.Add("@SearchValue", queryParameters.SearchValue);
            parameters.Add("@SortColumn", queryParameters.SortColumn);
            parameters.Add("@SortOrder", queryParameters.SortOrder);
            parameters.Add("@PageSize", queryParameters.PageSize);
            parameters.Add("@PageNo", queryParameters.PageNo);

            parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // get paginated jobs
            var jobs = (await connection.QueryAsync<Job, Industry, Employer, SavedJob>(
                sql,
                (job, industry, employer) =>
                {
                    SavedJob savedJob = new();
                    job.Industry = industry;
                    job.Employer = employer;
                    savedJob.Job = job;
                    savedJob.JobId = job.JobId;
                    return savedJob;
                },
                param: parameters,
                splitOn: "IndustryId,EmployerId",
                commandType: CommandType.StoredProcedure
            )).ToList();

            if (jobs.Any())
            {
                var jobIds = string.Join(",", jobs.Select(j => j.JobId));
                var jobSkills = await GetJobSkillsAsync(jobIds);
                foreach (var job in jobs)
                {
                    job.Job.Skills = jobSkills.Where(js => js.JobId == job.JobId);
                }
            }

            var totalCount = parameters.Get<int>("@TotalCount");

            // Combine results
            var paginatedList = new PaginatedList<SavedJob>(
                    jobs,
                    parameters.Get<int>("@TotalCount"),
                    queryParameters.PageNo,
                    queryParameters.PageSize
                );

            return paginatedList;
        }

        public async Task<int?> UpdateJobStatusAsync(int jobId, bool status, Guid employerId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[UpdateJobStatus]";
            var parameters = new DynamicParameters();
            parameters.Add("@JobId", jobId);
            parameters.Add("@IsActive", status);
            parameters.Add("@UserId", employerId);
            var result = await connection.ExecuteScalarAsync<int?>(
            sql, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
