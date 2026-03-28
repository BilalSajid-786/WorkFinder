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
    /// Implementation for Country Code Repository
    /// </summary>
    public class CountryCodeRepository : ICountryCodeRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public CountryCodeRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }
        /// <summary>
        /// Get all country codes from the db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CountryCode>> GetAllCountryCode()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[dbo].[GetAllCountryCode]";
            return await connection.QueryAsync<CountryCode>(sql, commandType:System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Insert a country code into the db
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public async Task<int> InsertCountryCode(CountryCode countryCode)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[dbo].[InsertCountryCode]";

            var parameters = new DynamicParameters();
            parameters.Add("CountryCodeId", countryCode.CountryCodeId);
            parameters.Add("CallingCode", countryCode.CallingCode);

            return await connection.ExecuteScalarAsync<int>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure); ;
        }
    }
}
