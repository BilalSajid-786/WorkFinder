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

        //public async Task<bool> DeleteEmployerAsync(Guid employerId)
        //{
        //    using var connection = _dapperDbContext.CreateConnection();
        //    var sql = "[DeleteEmployer]";

        //    var rowsAffected = await connection.ExecuteScalarAsync<int>(
        //        sql,
        //        new { EmployerId = employerId },
        //        commandType: CommandType.StoredProcedure
        //    );

        //    return rowsAffected > 0; // true if a row was updated, false if none matched
        //}

        public async Task<string?> EditEmployerAsync(Employer employer)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@EmployerId", employer.EmployerId);
            parameters.Add("@CompanyName", employer.CompanyName);
            parameters.Add("@WebsiteUrl", employer.WebsiteUrl);
            parameters.Add("@CompanySize", employer.CompanySize);
            parameters.Add("@ContactPerson", employer.ContactPerson);
            parameters.Add("@RegistrationNumber", employer.RegistrationNumber);
            parameters.Add("@IndustryId", employer.IndustryId);

            var status = await connection.ExecuteScalarAsync<string?>(
            "UpdateEmployer", parameters, commandType: CommandType.StoredProcedure);
            return status;
        }

        public async Task<IEnumerable<Employer>> GetAllemployers()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetAllEmployers]";
            var employers = await connection.QueryAsync<Employer, User, Role, Industry, Employer>(
                sql,
                (employer, user, role, industry) =>
                {
                    employer.User = user;
                    employer.User.Role = role;
                    employer.Industry = industry;
                    return employer;
                },
                splitOn: "UserId,RoleId,IndustryId",
                commandType: CommandType.StoredProcedure
            );

            return employers;
        }

        public async Task<Employer?> GetEmployerByIdAsync(Guid employerId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetEmployerById]";

            var employer = await connection.QueryAsync<Employer, User, Role, Industry, Employer>(
                sql,
                (emp, user, role, industry) =>
                {
                    emp.User = user;
                    emp.User.Role = role;
                    emp.Industry = industry;
                    return emp;
                },
                new { EmployerId = employerId },
                splitOn: "UserId,RoleId,IndustryId",
                commandType: CommandType.StoredProcedure
            );

            return employer.FirstOrDefault();
        }

        /// <summary>
        /// Gets an employerId for a given userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>EmployerId</returns>
        public async Task<Guid?> GetEmployerIdAsync(Guid userId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            
            //procedure name
            var sql = "[GetEmployerId]";

            //parameters name
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            return await connection.ExecuteScalarAsync<Guid?>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
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

        //public async Task<bool?> UpdateEmployerStatusAsync(Guid employerId, bool isActive)
        //{
        //    using var connection = _dapperDbContext.CreateConnection();
        //    var sql = "[UpdateEmployerStatus]";

        //    var updatedStatus = await connection.ExecuteScalarAsync<bool?>(
        //        sql,
        //        new { EmployerId = employerId, IsActive = isActive },
        //        commandType: CommandType.StoredProcedure
        //    );

        //    return updatedStatus; // true = Active, false = Inactive, null = User not found
        //}
    }
}
