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
        Task<User> RegisterUserAsync(User user);

        /// <summary>
        /// Gets a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User</returns>
        Task<User?> GetUserByEmailAsync(string email);
    }
}
