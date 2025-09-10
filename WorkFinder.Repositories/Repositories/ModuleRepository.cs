using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.Entities.Entities.SystemSeeding;
using WorkFinder.Repositories.DbContext;
using WorkFinder.RepositoryContracts;

namespace WorkFinder.Repositories.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public ModuleRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }
        /// <summary>
        /// Get all modules from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Module>> GetAllModulesAsync()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetAllModules]";
            return await connection.QueryAsync<Module>(sql,commandType:System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get modules for user by role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Modules</returns>
        public async Task<IEnumerable<Permission>> GetSideBarItemsAsync(Guid roleId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetModules]";
            var parameters = new DynamicParameters();
            parameters.Add("@RoleId", roleId);
            return await connection.QueryAsync<Module, Permission, Permission>(sql, (module, permission) =>
            {
                if (permission is null)
                    permission = new Permission();
                permission.Module = module;
                return permission;
            },
            parameters,
            splitOn: "PermissionId",
            commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// seed modules to the database
        /// </summary>
        /// <returns></returns>
        public async Task SeedModulesAsync()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var modules = await GetAllModulesAsync();
            if(!modules.Any()) 
            {
                var insertionSql = "[InsertModule]";
                foreach(var module in SystemModules.GetAllModules())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ModuleId", module.ModuleId);
                    parameters.Add("@ModuleName", module.ModuleName);
                    parameters.Add("@Route", module.Route);
                    parameters.Add("@ParentModuleId", module.ParentModuleId);
                    await connection.ExecuteScalarAsync(insertionSql, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
        }
    }
}
