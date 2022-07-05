using AutoMapper;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.Domain.Models;
using Exadel.Forecast.Models.Configuration;
using System.Linq;

namespace Exadel.Forecast.Api.Services
{
    public class ForecastService : IForecastService
    {
        private readonly IMapper _mapper;

        public ForecastService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<WeatherForecastDTO>> GetForecast(ForecastQueryDTO queryDTO)
        {
            var configuration = new Configuration()
            {
                MinAmountOfDays = 1, // int.Parse(System.Configuration.ConfigurationManager.AppSettings["MinValue"]),
                MaxAmountOfDays = 16, // int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxValue"]),
                OpenWeatherKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY"),
                WeatherApiKey = Environment.GetEnvironmentVariable("WEATHERAPI_API_KEY"),
                WeatherBitKey = Environment.GetEnvironmentVariable("WEATHERBIT_API_KEY"),
                DebugInfo = true, // bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["DebugInfo"], out bool value) && value,
                ExecutionTime = 10000 // int.Parse(System.Configuration.ConfigurationManager.AppSettings["ExecutionTime"])
            };
            configuration.SetDefaultForecastApi(ForecastApi.WeatherBit);
            string cities = string.Join(",", queryDTO.Cities);
            var weatherCommand = new WeatherCommand(cities, configuration, queryDTO.Days);
            var forecastList = await weatherCommand.GetResultAsync();
            List<WeatherForecastDTO> dtoList = forecastList.Select(p => _mapper.Map<WeatherForecastDTO>(p.Model)).ToList();
            return dtoList;
        }
    }
}
