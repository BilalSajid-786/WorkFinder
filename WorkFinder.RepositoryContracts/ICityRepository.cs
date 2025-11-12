using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contract for City Entity data operations
    /// </summary>
    public interface ICityRepository
    {
        /// <summary>
        /// Inserts a new city in the database
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        Task<int> InsertCity(City city);

        /// <summary>
        /// Get all cities from the database
        /// </summary>
        /// <returns>Cities</returns>
        Task<IEnumerable<City>> GetCities();

        /// <summary>
        /// Get cities by country id
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<City>> GetCitiesByCountryIdAsync(int countryId);
    }
}
