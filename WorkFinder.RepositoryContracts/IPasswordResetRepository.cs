using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository contract for reset password
    /// </summary>
    public interface IPasswordResetRepository
    {
        /// <summary>
        /// Create a password reset request in the system
        /// </summary>
        /// <param name="passwordResetRequest"></param>
        /// <returns></returns>
        Task<int> CreatePasswordResetRequest(PasswordResetRequest passwordResetRequest);
        
        /// <summary>
        /// Mark a request as used after getting used
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task MarkAsUsed(int id);

        /// <summary>
        /// Check is the given token a valid token or not for the user.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<PasswordResetRequest> IsValidToken(string token);
    }
}
