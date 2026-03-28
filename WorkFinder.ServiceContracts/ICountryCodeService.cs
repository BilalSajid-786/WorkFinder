using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.CountryCode;

namespace WorkFinder.ServiceContracts
{
    public interface ICountryCodeService
    {
        Task<IEnumerable<CountryCodeResponseDto>> GetAllCountryCode();

        Task SeedCountryCode();

    }
}
