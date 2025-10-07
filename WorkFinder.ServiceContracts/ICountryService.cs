using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Country;

namespace WorkFinder.ServiceContracts
{
    public interface ICountryService
    {
        /// <summary>
        /// Seed countries into system
        /// </summary>
        /// <returns></returns>
        Task SeedCountriesAsync();

        /// <summary>
        /// Get countries from system
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CountryResponseDto>> GetCountriesAsync();
        
    }
}
