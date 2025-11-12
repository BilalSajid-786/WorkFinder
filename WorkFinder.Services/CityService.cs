using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.Entities.Entities.SystemSeeding;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.City;
using WorkFinder.ServiceContracts.DTOs.Country;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for City
    /// </summary>
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get City from the system
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CityResponseDto>> GetCitiesAsync()
        {
            var cities = await _cityRepository.GetCities();
            return _mapper.Map<IEnumerable<CityResponseDto>>(cities);
        }

        /// <summary>
        /// Get cities by country id
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CityResponseDto>> GetCitiesByCountryIdAsync(int countryId)
        {
            var cities = await _cityRepository.GetCitiesByCountryIdAsync(countryId);
            return _mapper.Map<IEnumerable<CityResponseDto>>(cities);
        }

        /// <summary>
        /// Seed cities into system
        /// </summary>
        /// <returns></returns>

        public async Task SeedCitiesAsync()
        {
            var cities = await _cityRepository.GetCities();
            if (cities.Count() == 0)
            {
                foreach (var city in SystemCities.Cities)
                {
                    await _cityRepository.InsertCity(new City()
                    {
                        CityId = city.CityId,
                        CityName = city.CityName,
                        CountryId = city.CountryId
                    });
                }
            }
        }
    }
}
