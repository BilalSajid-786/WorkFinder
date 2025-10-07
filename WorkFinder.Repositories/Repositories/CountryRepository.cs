using Dapper;
using WorkFinder.Entities.Entities;
using WorkFinder.Repositories.DbContext;
using WorkFinder.RepositoryContracts;

namespace WorkFinder.Repositories.Repositories
{
    /// <summary>
    /// Repository Implementation for Country
    /// </summary>
    public class CountryRepository : ICountryRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public CountryRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }
        public async Task<IEnumerable<Country>> GetCountries()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetCountry]";
            return await connection.QueryAsync<Country>(sql,commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<int> InsertCountry(Country country)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[InsertCountry]";
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", country.CountryId);
            parameters.Add("@CountryName",country.CountryName);
            return await connection.ExecuteScalarAsync<int>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
