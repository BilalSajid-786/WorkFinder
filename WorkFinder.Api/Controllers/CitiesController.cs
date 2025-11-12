using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Response;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ResponseDto _responseDto;
        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
            _responseDto = new();
        }

        /// <summary>
        /// Get Cities from the system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetCitiesAsync()
        {
            try
            {
                var cities = await _cityService.GetCitiesAsync();
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = cities;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet("cities/by-country/{countryId:int}")]
        public async Task<ActionResult<ResponseDto>> GetCitiesByCountryIdAsync(int countryId)
        {
            try
            {
                var cities = await _cityService.GetCitiesByCountryIdAsync(countryId);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = cities;   // e.g., IEnumerable<CityDto> or City[]
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
