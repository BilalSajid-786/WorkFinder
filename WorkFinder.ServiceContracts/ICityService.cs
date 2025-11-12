using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.City;

namespace WorkFinder.ServiceContracts
{
    public interface ICityService
    {
        /// <summary>
        /// Seed cities into system
        /// </summary>
        /// <returns></returns>
        Task SeedCitiesAsync();

        /// <summary>
        /// Get cities from system
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CityResponseDto>> GetCitiesAsync();

        /// <summary>
        /// Get cities by country id
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CityResponseDto>> GetCitiesByCountryIdAsync(int countryId);
    }
}
