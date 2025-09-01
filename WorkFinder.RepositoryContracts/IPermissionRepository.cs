using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    public interface IPermissionRepository
    {
        /// <summary>
        /// Seeds permissions to the database
        /// </summary>
        /// <returns></returns>
        Task SeedPermissionsAsync();

        /// <summary>
        /// Get all permissions from the database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
    }
}
