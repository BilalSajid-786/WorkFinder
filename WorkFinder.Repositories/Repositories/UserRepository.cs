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
            var users = await context.QueryAsync<User>(sql);
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
            var user = await connection.QueryFirstAsync<User>("SELECT * FROM Users WHERE Email = @Email", new {Email = email});
            return user;
        }

        /// <summary>
        /// Inserts a new user in the database
        /// </summary>
        /// <param name="user">User to be inserted</param>
        /// <returns>Newly inserted User</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Guid> RegisterUserAsync(User user)
        {
            using var connection = _dapperDbContext.CreateConnection();

            //Parameters
            var parameters = new DynamicParameters();
            parameters.Add("@Name", user.Name);
            parameters.Add("@Email", user.Email);
            parameters.Add("@PasswordHash", user.PasswordHash);

            return await connection.ExecuteScalarAsync<Guid>("InsertUser",parameters);
        }
    }
}
