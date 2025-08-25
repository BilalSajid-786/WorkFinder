using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contract for Country Entity data operations
    /// </summary>
    public interface ICountryRepository
    {
        /// <summary>
        /// Inserts a new country in the database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        Task InsertCountry(Country country);

        /// <summary>
        /// Get all countries from the database
        /// </summary>
        /// <returns>Countries</returns>
        Task<IEnumerable<Country>> GetCountries();
    }
}
