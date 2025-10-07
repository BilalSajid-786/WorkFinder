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
    /// Repository Implementation for Qualification
    /// </summary>
    public class QualificationRepository : IQualificationRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public QualificationRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        /// <summary>
        /// Get all qualifications from db
        /// </summary>
        /// <returns>Inserted Qualification</returns>
        public async Task<IEnumerable<Qualification>> GetAllQualifications()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetQualification]";
            return await connection.QueryAsync<Qualification>(sql, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Insert qualification into db
        /// </summary>
        /// <param name="qualification"></param>
        /// <returns></returns>
        public async Task<int> InsertQualification(Qualification qualification)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[InsertQualification]";
            var parameters = new DynamicParameters();
            parameters.Add("@QualificationId", qualification.QualificationId);
            parameters.Add("@QualificationName", qualification.QualificationName);
            return await connection.ExecuteScalarAsync<int>(sql,parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
