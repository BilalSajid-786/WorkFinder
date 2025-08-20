using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
