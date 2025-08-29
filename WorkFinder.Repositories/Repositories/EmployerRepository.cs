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

        public async Task<bool> DeleteEmployerAsync(Guid userId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[DeleteEmployer]";

            var rowsAffected = await connection.ExecuteScalarAsync<int>(
                sql,
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0; // true if a row was updated, false if none matched
        }

        public async Task<int> EditEmployerAsync(Guid userId, Employer employer)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@UserName", employer.CompanyName);
            parameters.Add("@Email", employer.Email);
            parameters.Add("@City", employer.City);
            parameters.Add("@Country", employer.Country);
            parameters.Add("@Phone", employer.Phone);

            parameters.Add("@CompanyName", employer.CompanyName);
            parameters.Add("@WebsiteUrl", employer.WebsiteUrl);
            parameters.Add("@CompanySize", employer.CompanySize);
            parameters.Add("@ContactPerson", employer.ContactPerson);
            parameters.Add("@RegistrationNumber", employer.RegistrationNumber);
            parameters.Add("@IndustryId", employer.IndustryId);

            var rowsAffected = await connection.ExecuteScalarAsync<int>(
            "UpdateEmployer", parameters, commandType: CommandType.StoredProcedure);
            return rowsAffected;
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

        public async Task<Employer?> GetEmployerByIdAsync(Guid userId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetEmployerById]";

            var employer = await connection.QueryAsync<Employer, Role, Industry, Employer>(
                sql,
                (emp, role, industry) =>
                {
                    emp.Role = role;
                    emp.Industry = industry;
                    return emp;
                },
                new { UserId = userId },
                splitOn: "RoleId,IndustryId",
                commandType: CommandType.StoredProcedure
            );

            return employer.FirstOrDefault();
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

        public async Task<bool?> UpdateEmployerStatusAsync(Guid userId, bool isActive)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[UpdateEmployerStatus]";

            var updatedStatus = await connection.ExecuteScalarAsync<bool?>(
                sql,
                new { UserId = userId, IsActive = isActive },
                commandType: CommandType.StoredProcedure
            );

            return updatedStatus; // true = Active, false = Inactive, null = User not found
        }
    }
}
