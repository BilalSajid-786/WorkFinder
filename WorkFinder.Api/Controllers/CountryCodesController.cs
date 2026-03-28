using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.CountryCode;
using WorkFinder.ServiceContracts.DTOs.Industry;
using WorkFinder.ServiceContracts.DTOs.Response;
using WorkFinder.Services;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryCodesController : ControllerBase
    {
        private readonly ICountryCodeService _countryCodeService;
        private readonly ResponseDto _responseDto;
        public CountryCodesController(ICountryCodeService countryCodeService)
        {
            _countryCodeService = countryCodeService;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<CountryCodeResponseDto>> GetCountryCodes()
        {
            try
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = await _countryCodeService.GetAllCountryCode();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return Ok(_responseDto);
        }
    }
}
