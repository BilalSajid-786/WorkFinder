using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Authentication;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Service contract for forgot password
    /// </summary>
    public interface IForgotPasswordService
    {
        /// <summary>
        /// Send a password reset email to given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task SendPasswordResetEmail(string email);

        /// <summary>
        /// Reset the password for the user
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns></returns>
        Task ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
