using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contract for Role Entity data operations
    /// </summary>
    public interface IRoleRepository
    {
        Task SeedRolesAsync();
    }
}
