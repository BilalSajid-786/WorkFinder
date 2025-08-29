using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.User;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> GenerateToken(UserResponseDto user);
    }
}
