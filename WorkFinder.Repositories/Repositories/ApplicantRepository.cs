using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task AddApplicantSkillAsync(Skill skill, Guid applicantId)
        {
            using var connection = _dapperDbContext.CreateConnection();

            //procedure name
            var sql = "[InsertApplicantSkill]";

            //procedure parameters
            var parameters = new DynamicParameters();
            parameters.Add("@ApplicantId", applicantId);
            parameters.Add("@SkillId", skill.SkillId);
            parameters.Add("@SkillName", skill.SkillName);

            await connection.ExecuteAsync(sql, parameters);
        }

        /// <summary>
        /// Get ApplicantId from the system
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>ApplicantId</returns>
        public async Task<Guid?> GetApplicantIdAsync(Guid userId)
        {
            using var connection = _dapperDbContext.CreateConnection();

            //procedure name
            var sql = "[GetApplicantId]";

            //procedure parameters
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            return await connection.ExecuteScalarAsync<Guid>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
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
            parameters.Add("@Gender", applicant.Gender);
            parameters.Add("@QualificationId", applicant.QualificationId);

            return await connection.ExecuteScalarAsync<Guid>(sql,parameters,commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Check is applicant exists in the system
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        public async Task<bool> IsApplicantExistAsync(Guid applicantId)
        {
            using var connection = _dapperDbContext.CreateConnection();

            //procedure name
            var sql = "[IsApplicantExist]";
            var parameters = new DynamicParameters();
            parameters.Add("@ApplicantId", applicantId);

            return await connection.ExecuteScalarAsync<bool>(sql,parameters,commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Updates a resume for an applicant in db
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        public async Task UpdateApplicantResume(string resumeName, Guid applicantId)
        {
            using var connection = _dapperDbContext.CreateConnection();

            //procedure name
            var sql = "[UpdateApplicantResume]";
            var parameters = new DynamicParameters();
            parameters.Add("@Resume", resumeName);
            parameters.Add("@ApplicantId", applicantId);

            await connection.ExecuteScalarAsync<int>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
