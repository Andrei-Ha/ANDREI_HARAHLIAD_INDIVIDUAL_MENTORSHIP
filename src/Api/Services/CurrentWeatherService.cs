using AutoMapper;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.Models.Configuration;

namespace Exadel.Forecast.Api.Services
{
    public class CurrentWeatherService : ICurrentWeatherService
    {
        private readonly IMapper _mapper;

        public CurrentWeatherService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<CurrentWeatherDTO>> GetCurrentWeather(CurrentQueryDTO queryDTO)
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

            configuration.SetDefaultForecastApi(queryDTO.ForecastApi);
            string cities = string.Join(",", queryDTO.Cities);
            var weatherCommand = new WeatherCommand(cities, configuration, 0);
            var currentWeatherList = await weatherCommand.GetResultAsync();
            List<CurrentWeatherDTO> dtoList = currentWeatherList.Select(p => _mapper.Map<CurrentWeatherDTO>(p.Model)).ToList();
            return dtoList;
        }
    }
}
