using AutoMapper;
using WorkFinder.Entities.Entities.SystemSeeding;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Country;
using WorkFinder.Entities.Entities;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for Country
    /// </summary>
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Countries from the system
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CountryResponseDto>> GetCountriesAsync()
        {
            var countries = await _countryRepository.GetCountries();
            return _mapper.Map<IEnumerable<CountryResponseDto>>(countries);
        }

        /// <summary>
        /// Seed countries into system
        /// </summary>
        /// <returns></returns>
        public async Task SeedCountriesAsync()
        {
            var countries = await _countryRepository.GetCountries();
            if (countries.Count() == 0)
            {
                foreach (var country in SystemCountries.Countries)
                {
                    await _countryRepository.InsertCountry(new Country()
                    {
                        CountryId = country.Key,
                        CountryName = country.Value
                    });
                }
            }
        }
    }
}
