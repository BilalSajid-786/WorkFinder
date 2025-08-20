using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.ServiceContracts.DTOs;

namespace WorkFinder.ServiceContracts
{
    public interface IUserService
    {
        /// <summary>
        /// Registers a user
        /// </summary>
        /// <returns></returns>
        Task<Guid> RegisterUserAsync(RegisterRequestDto registerRequestDto,string passwordHash);

        /// <summary>
        /// Gets a user by an email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User</returns>
        Task<User?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Gets all existing users.
        /// </summary>
        /// <returns>All Users</returns>
        Task<IEnumerable<User>> GetAllUsers();
    }
}
