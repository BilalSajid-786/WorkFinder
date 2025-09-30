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
    /// Repository Implementation for Industry
    /// </summary>
    public class IndustryRepository : IIndustryRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public IndustryRepository(DapperDbContext dapperDbContext) { 
            _dapperDbContext = dapperDbContext;
        }

        /// <summary>
        /// Get all industries from the db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Industry>> GetIndustries()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetIndustries]";
            return await connection.QueryAsync<Industry>(sql, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get industry against a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Industry?> GetIndustryByIdAsync(int id)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetIndustryById]";
            var parameters = new DynamicParameters();
            parameters.Add("@IndustryId", id);
            return await connection.QuerySingleOrDefaultAsync<Industry>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Insert industry into db
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        public async Task InsertIndustry(Industry industry)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[InsertIndustry]";
            var parameters = new DynamicParameters();
            parameters.Add("@IndustryName", industry.IndustryName);
            await connection.ExecuteScalarAsync<int>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
