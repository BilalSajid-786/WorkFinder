using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Authentication Service
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user by email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Jwt token</returns>
        Task<string?> AuthenticateAsync(string email, string password);
    }
}
