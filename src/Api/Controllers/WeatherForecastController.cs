using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.BL.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.Forecast.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetHistory([FromQuery] HistoryQueryDTO historyQueryDTO)
        {
            var timeIntervalValidator = new TimeIntervalValidator();

            if (!timeIntervalValidator.IsValid(historyQueryDTO.StartDateTime, historyQueryDTO.EndDateTime))
            {
                return BadRequest();
            }

            return Ok(await _historyService.Get(historyQueryDTO));
        }
    }
}