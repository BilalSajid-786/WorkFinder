using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contract for CountryCode
    /// </summary>
    public interface ICountryCodeRepository
    {
        /// <summary>
        /// Insert CountryCode into db
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        Task<int> InsertCountryCode(CountryCode countryCode);

        /// <summary>
        /// Get All Country CallingCodes
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CountryCode>> GetAllCountryCode();
    }
}
