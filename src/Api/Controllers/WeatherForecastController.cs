using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.Forecast.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService<WeatherForecastDTO, ForecastQueryDTO> _forecastService;
        private readonly IWeatherService<CurrentWeatherDTO, CurrentQueryDTO> _currentService;

        public WeatherForecastController(
            IWeatherService<WeatherForecastDTO, ForecastQueryDTO> forecastService,
            IWeatherService<CurrentWeatherDTO, CurrentQueryDTO> currentService)
        {
            _forecastService = forecastService;
            _currentService = currentService;

        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ForecastQueryDTO forecastQueryDTO)
        {
            return Ok(await _forecastService.Get(forecastQueryDTO));
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent([FromQuery]CurrentQueryDTO currentQueryDTO)
        {
            return Ok(await _currentService.Get(currentQueryDTO));
        }
    }
}