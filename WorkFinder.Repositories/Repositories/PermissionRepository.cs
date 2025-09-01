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
    public class PermissionRepository : IPermissionRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public PermissionRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }
        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetAllPermissions]";
            return await connection.QueryAsync<Permission>(sql, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task SeedPermissionsAsync()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var permissions = await GetAllPermissionsAsync();
            if(!permissions.Any()) 
            {
                var insertionSql = "[InsertPermission]";
                foreach (var permission in SystemPermissions.GetAllPermissions())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@PermissionId", permission.PermissionId);
                    parameters.Add("@ModuleId", permission.ModuleId);
                    parameters.Add("@Action", permission.Action);
                    await connection.ExecuteScalarAsync<int>(insertionSql, parameters,commandType: System.Data.CommandType.StoredProcedure);
                }
            }
        }
    }
}
