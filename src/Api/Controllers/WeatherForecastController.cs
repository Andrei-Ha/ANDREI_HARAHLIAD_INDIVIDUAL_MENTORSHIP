using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.Forecast.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService<WeatherForecastDTO, ForecastQueryDTO> _forecastService;
        private readonly IWeatherService<CurrentWeatherDTO, CurrentQueryDTO> _currentService;
        private readonly IWeatherService<WeatherHistoryDTO, HistoryQueryDTO> _historyService;

        public WeatherForecastController(
            IWeatherService<WeatherForecastDTO, ForecastQueryDTO> forecastService,
            IWeatherService<CurrentWeatherDTO, CurrentQueryDTO> currentService,
            IWeatherService<WeatherHistoryDTO, HistoryQueryDTO> historyService)
        {
            _forecastService = forecastService;
            _currentService = currentService;
            _historyService = historyService;
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

        [HttpGet("history")]
        [Authorize(Policy = "PostmanUser")]
        public async Task<IActionResult> GetHistory([FromQuery] HistoryQueryDTO historyQueryDTO)
        {
            return Ok(await _historyService.Get(historyQueryDTO));
        }

        [HttpGet("subscribtion")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Subscription([FromQuery] SubscriptionModel statisticDTO)
        {
            return Ok();
        }
    }
}