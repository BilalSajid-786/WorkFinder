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
    /// Repository Implementation for managing roles
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public RoleRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        /// <summary>
        /// Seed roles in the db, if roles doesn't exist
        /// </summary>
        /// <returns></returns>
        public async Task SeedRolesAsync()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetAllRoles]";
            var roles = await connection.QueryAsync<Role>(sql);
            if (!roles.Any())
            {
                var insertionSql = "[InsertRole]";

                var newRoles = new Dictionary<Guid, string>() { {SystemRoles.AdminId, SystemRoles.Admin },
                    { SystemRoles.EmployerId, SystemRoles.Employer},
                    { SystemRoles.ApplicantId, SystemRoles.Applicant} };

                foreach (var role in newRoles)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@RoleId", role.Key);
                    parameters.Add("@Name", role.Value);
                    await connection.ExecuteScalarAsync<Guid>(insertionSql, parameters);
                }
            }

        }
    }
}
