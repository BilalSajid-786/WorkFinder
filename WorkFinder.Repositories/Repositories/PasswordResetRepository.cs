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
    /// Repository Implementation for password reset
    /// </summary>
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public PasswordResetRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }
        /// <summary>
        /// Create a password reset request
        /// </summary>
        /// <param name="passwordResetRequest"></param>
        /// <returns></returns>
        public async Task<int> CreatePasswordResetRequest(PasswordResetRequest passwordResetRequest)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[CreatePasswordResetRequest]";
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", passwordResetRequest.UserId);
            parameters.Add("@Token", passwordResetRequest.Token);
            parameters.Add("@ExpiresAt", passwordResetRequest.ExpiresAt);
            parameters.Add("@Used", passwordResetRequest.Used);
            return await connection.ExecuteScalarAsync<int>(sql, parameters, 
                commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Is a given token valid or not
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<PasswordResetRequest> IsValidToken(string token)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[IsValidToken]";
            var parameters = new DynamicParameters();
            parameters.Add("@Token", token);
            return await connection.QueryFirstAsync<PasswordResetRequest>(sql, parameters,
                commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Mark the password reset request to used status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task MarkAsUsed(int id)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[MarkAsUsed]";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            await connection.ExecuteAsync(sql, parameters,
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
