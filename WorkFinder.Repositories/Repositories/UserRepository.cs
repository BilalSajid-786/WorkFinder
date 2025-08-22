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
    /// Repository implementation for managing User entity data operations
    /// Provides methods to register and retrieve users from database
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public UserRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        /// <summary>
        /// Gets all users from the database
        /// </summary>
        /// <returns>List of Users</returns>
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using var context = _dapperDbContext.CreateConnection();
            var sql = "[GetAllUsers]";
            var users = await context.QueryAsync<User, Role, User>(sql,
                (user, role) =>
                {
                    user.Role = role;
                    return user;
                },
                splitOn: "RoleId",
                commandType: System.Data.CommandType.StoredProcedure);
            return users;
        }

        /// <summary>
        /// Gets a user from database based on the given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User</returns>
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var user = await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new {Email = email});
            return user;
        }

        /// <summary>
        /// Gets a password hash of a user from the database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Password Hash</returns>
        public async Task<string?> GetUserPasswordHashById(Guid userId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetUserPasswordHash]";
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            return await connection.QuerySingleOrDefaultAsync<string>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Inserts a new user in the database
        /// </summary>
        /// <param name="user">User to be inserted</param>
        /// <returns>Newly inserted User</returns>
        public async Task<Guid> RegisterUserAsync(User user)
        {
            using var connection = _dapperDbContext.CreateConnection();

            //Parameters
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", user.UserName);
            parameters.Add("@Email", user.Email);
            parameters.Add("@PasswordHash", user.PasswordHash);
            parameters.Add("@RoleId", user.RoleId);

            return await connection.ExecuteScalarAsync<Guid>("InsertUser",parameters,
                commandType:System.Data.CommandType.StoredProcedure);
        }
    }
}
