using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace WorkFinder.Repositories.DbContext
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("WorkFinderDb");
        }

        public IDbConnection CreateConnection() => 
            new SqlConnection(_connectionString);
    }
}
