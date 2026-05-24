using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Response;

namespace WorkFinder.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ResponseDto _responseDto;
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
            _responseDto = new();
        }

        /// <summary>
        /// Get Countries from the system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetCountriesAsync()
        {
            try
            {
                var countries = await _countryService.GetCountriesAsync();
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = countries;
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
