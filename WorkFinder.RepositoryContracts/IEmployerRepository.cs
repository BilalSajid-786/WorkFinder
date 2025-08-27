using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    public interface IEmployerRepository
    {
        /// <summary>
        /// Inserts a new employer in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Guid> RegisterEmployerAsync(Employer employer);

        /// <summary>
        /// Gets all employers from database
        /// </summary>
        /// <returns>List of employers</returns>
        Task<IEnumerable<Employer>> GetAllemployers();
    }
}
