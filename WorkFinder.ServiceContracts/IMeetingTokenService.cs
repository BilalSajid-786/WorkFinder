using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Service contract to get zoom token
    /// </summary>
    public interface IMeetingTokenService
    {
        Task<string?> GetAccessTokenAsync();
    }
}
