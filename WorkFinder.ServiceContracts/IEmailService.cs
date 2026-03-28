using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Service contract for email
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// send reset password email to given email address
        /// </summary>
        /// <param name="to"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        Task SendPasswordResetEmail(string to, string link);

        /// <summary>
        /// Send verification email to the users
        /// </summary>
        /// <param name="to"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        Task SendVerificationEmail(string to, string link);
    }
}
