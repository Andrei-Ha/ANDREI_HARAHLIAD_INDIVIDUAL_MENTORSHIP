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

        public WeatherForecastController(IForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ForecastQueryDTO forecastQueryDTO)
        {
            return Ok(await _forecastService.GetForecast(forecastQueryDTO));
        }
    }
}