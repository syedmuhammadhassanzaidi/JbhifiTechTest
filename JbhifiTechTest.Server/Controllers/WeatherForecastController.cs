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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                //Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
