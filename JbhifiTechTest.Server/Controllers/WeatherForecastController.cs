using Microsoft.AspNetCore.Mvc;
using JbhifiTechTest.Server.Services;

namespace JbhifiTechTest.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IOpenWeatherMapService _openWeatherMapService;


        public WeatherForecastController(IOpenWeatherMapService openWeatherMapService)
        {
            _openWeatherMapService = openWeatherMapService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string city, string country)
        {
            try
            {
                if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
                {
                    return BadRequest("City and Country are required values");
                }

                var desc = await _openWeatherMapService.GetWeatherDetailsAsync(city, country);

                return Ok(desc);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
