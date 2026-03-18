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
    /// Implementation for School Degree Repository
    /// </summary>
    public class SchoolDegreeRepository : ISchoolDegreeRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public SchoolDegreeRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        public async Task<IEnumerable<SchoolDegree>> GetAllSchoolDegrees()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetAllSchoolDegrees]";
            return await connection.QueryAsync<SchoolDegree>(sql, commandType: System.Data.CommandType.StoredProcedure);

        }

        public async Task<int> InsertSchoolDegree(SchoolDegree schoolDegree)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[InsertSchoolDegree]";
            var parameters = new DynamicParameters();
            parameters.Add("@SchoolDegreeId", schoolDegree.SchoolDegreeId);
            parameters.Add("@SchoolDegreeName", schoolDegree.SchoolDegreeName);
            return await connection.ExecuteScalarAsync<int>(sql,parameters, commandType: System.Data.CommandType.StoredProcedure);

        }
    }
}
