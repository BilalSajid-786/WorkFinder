using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contract for User Entity data operations
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Inserts a new user in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Guid> RegisterUserAsync(User user);

        /// <summary>
        /// Gets a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User</returns>
        Task<User?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Gets all users from database
        /// </summary>
        /// <returns>List of Users</returns>
        Task<IEnumerable<User>> GetAllUsers();

        /// <summary>
        /// Gets a password hash of a user for the given Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Password Hash</returns>
        Task<string?> GetUserPasswordHashById(Guid userId);
        Task<string?> EditUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<bool?> UpdateUserStatusAsync(Guid userId, bool isActive);
    }
}
