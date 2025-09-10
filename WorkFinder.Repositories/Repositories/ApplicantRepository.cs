using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.Repositories.DbContext;
using WorkFinder.RepositoryContracts;

namespace WorkFinder.Repositories.Repositories
{
    /// <summary>
    /// Repository Implementation for applicants
    /// </summary>
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public ApplicantRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        /// <summary>
        /// Insert skill for an applicant
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="ApplicantId"></param>
        public async Task AddApplicantSkillAsync(int skillId, Guid applicantId)
        {
            using var connection = _dapperDbContext.CreateConnection();

            //procedure name
            var sql = "[InsertApplicantSkill]";

            //procedure parameters
            var parameters = new DynamicParameters();
            parameters.Add("@ApplicantId", applicantId);
            parameters.Add("@SkillId", skillId);

            await connection.ExecuteAsync(sql, parameters);
        }

        /// <summary>
        /// Insert applicants into the database
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns>Applicant Id</returns>
        public async Task<Guid> InsertApplicantAsync(Applicant applicant)
        {
            using var connection = _dapperDbContext.CreateConnection();
            
            //procedure name
            var sql = "[InsertApplicant]";
            
            //procedure parameters
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", applicant.UserId);
            parameters.Add("@Resume", applicant.Resume);

            return await connection.ExecuteScalarAsync<Guid>(sql,parameters,commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
