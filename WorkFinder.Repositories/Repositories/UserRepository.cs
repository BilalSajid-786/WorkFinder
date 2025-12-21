using Dapper;
using System.Data;
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

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[DeleteUser]";

            var rowsAffected = await connection.ExecuteScalarAsync<int>(
                sql,
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0; // true if a row was updated, false if none matched
        }

        public async Task<string?> EditUserAsync(User user)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", user.UserId);
            parameters.Add("@UserName", user.UserName);
            parameters.Add("@Email", user.Email);
            parameters.Add("@PasswordHash", user.Password.Length > 0 ? user.Password : null);
            parameters.Add("@City", user.City);
            parameters.Add("@Country", user.Country);
            parameters.Add("@Phone", user.Phone);

            var status = await connection.ExecuteScalarAsync<string?>(
            "UpdateUser", parameters, commandType: CommandType.StoredProcedure);
            return status;
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
            var sql = "[GetUserByEmail]";
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            var user = await connection.QueryAsync<User,Role,User>(sql,
                (user, role) =>
                {
                    user.Role = role;
                    return user;
                },
                parameters,
                splitOn: "RoleId",
                commandType: System.Data.CommandType.StoredProcedure);
            return user.SingleOrDefault();
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
            parameters.Add("@PasswordHash", user.Password);
            parameters.Add("@RoleId", user.RoleId);
            parameters.Add("@City", user.City);
            parameters.Add("@Country", user.Country);
            parameters.Add("@Phone", user.Phone);
            parameters.Add("@CreatedAt", user.CreatedAt);

            return await connection.ExecuteScalarAsync<Guid>("InsertUser",parameters,
                commandType:System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Update User Password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task UpdateUserPassword(string password, Guid userId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[UpdateUserPassword]";
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@Password", password);
            await connection.ExecuteAsync(sql, parameters,
                commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Update user profile pic in db
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="profilePicName"></param>
        /// <returns></returns>
        public async Task UpdateUserProfilePic(Guid userId, string profilePicName)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[UpdateUserProfilePic]";
            var parameters = new DynamicParameters();
            parameters.Add("@UserId",userId);
            parameters.Add("@ProfilePicName", profilePicName);
            await connection.ExecuteAsync(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<bool?> UpdateUserStatusAsync(Guid userId, bool isActive)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[UpdateUserStatus]";

            var updatedStatus = await connection.ExecuteScalarAsync<bool?>(
                sql,
                new { UserId = userId, IsActive = isActive },
                commandType: CommandType.StoredProcedure
            );

            return updatedStatus; // true = Active, false = Inactive, null = User not found
        }
    }
}
