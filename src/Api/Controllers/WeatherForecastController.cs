using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.Forecast.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IForecastService _forecastService;
        private readonly ICurrentWeatherService _currentWeatherService;

        public WeatherForecastController(IForecastService forecastService, ICurrentWeatherService currentWeatherService)
        {
            _forecastService = forecastService;
            _currentWeatherService = currentWeatherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ForecastQueryDTO forecastQueryDTO)
        {
            return Ok(await _forecastService.GetForecast(forecastQueryDTO));
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent([FromQuery]CurrentQueryDTO currentQueryDTO)
        {
            return Ok(await _currentWeatherService.GetCurrentWeather(currentQueryDTO));
        }
    }
}