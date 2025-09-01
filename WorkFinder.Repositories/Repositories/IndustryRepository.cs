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
    public class IndustryRepository : IIndustryRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public IndustryRepository(DapperDbContext dapperDbContext) { 
            _dapperDbContext = dapperDbContext;
        }

        public async Task<IEnumerable<Industry>> GetIndustries()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetIndustries]";
            return await connection.QueryAsync<Industry>(sql, commandType: System.Data.CommandType.StoredProcedure);
        }

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
