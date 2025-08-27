using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.Repositories.DbContext;
using WorkFinder.RepositoryContracts;

namespace WorkFinder.Repositories.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public EmployerRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        public async Task<IEnumerable<Employer>> GetAllemployers()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetAllEmployers]";
            var employers = await connection.QueryAsync<Employer, Role, Industry, Employer>(
                sql,
                (employer, role, industry) =>
                {
                    employer.Role = role;
                    employer.Industry = industry;
                    return employer;
                },
                splitOn: "RoleId,IndustryId",
                commandType: CommandType.StoredProcedure
            );

            return employers;
        }

        /// <summary>
        /// Inserts a new employer in the database
        /// </summary>
        /// <param name="employer">employer to be inserted</param>
        /// <returns>Newly inserted employer</returns>
        public async Task<Guid> RegisterEmployerAsync(Employer employer)
        {
            using var connection = _dapperDbContext.CreateConnection();

            //Parameters
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", employer.UserId);
            parameters.Add("@IndustryId", employer.IndustryId);
            parameters.Add("@CompanyName", employer.CompanyName);
            parameters.Add("@WebsiteUrl", employer.WebsiteUrl);
            parameters.Add("@CompanySize", employer.CompanySize);
            parameters.Add("@ContactPerson", employer.ContactPerson);
            parameters.Add("@RegistrationNumber", employer.RegistrationNumber);

            return await connection.ExecuteScalarAsync<Guid>("InsertEmployer", parameters,
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
