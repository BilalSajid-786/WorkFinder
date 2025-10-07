using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    public interface IIndustryRepository
    {
        /// <summary>
        /// Inserts a new industry in the database
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        Task InsertIndustry(Industry industry);

        /// <summary>
        /// Gets all industries from the database
        /// </summary>
        /// <returns>Industries</returns>
        Task<IEnumerable<Industry>> GetIndustries();

        /// <summary>
        /// Gets an industry detail by an id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Industry?> GetIndustryByIdAsync(int id);
    }
}
