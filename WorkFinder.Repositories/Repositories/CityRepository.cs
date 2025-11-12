using Dapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.Repositories.DbContext;
using WorkFinder.RepositoryContracts;

namespace WorkFinder.Repositories.Repositories
{
    /// <summary>
    /// Repository Implementation for City
    /// </summary>
    public class CityRepository : ICityRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public CityRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }
        public async Task<IEnumerable<City>> GetCities()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetCity]";
            return await connection.QueryAsync<City>(sql, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get cities by country id
        /// </summary>
        /// <returns></returns>

        public async Task<IEnumerable<City>> GetCitiesByCountryIdAsync(int countryId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetCitiesByCountryId]";
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", countryId);
            return await connection.QueryAsync<City>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<int> InsertCity(City city)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[InsertCity]";
            var parameters = new DynamicParameters();
            parameters.Add("@CityId", city.CityId);
            parameters.Add("@CityName", city.CityName);
            parameters.Add("@CountryId", city.CountryId);
            return await connection.ExecuteScalarAsync<int>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
