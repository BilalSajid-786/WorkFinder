using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts
{
    public interface IPermissionService
    {
        /// <summary>
        /// Seeds permissions to the system
        /// </summary>
        /// <returns></returns>
        Task SeedPermissionsAsync();
    }
}
