using AutoMapper;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.BL.Commands;

namespace Exadel.Forecast.Api.Services
{
    public class CurrentWeatherService : ICurrentWeatherService
    {
        private readonly IMapper _mapper;
        private readonly Models.Interfaces.IConfiguration _configuration;

        public CurrentWeatherService(IMapper mapper, Models.Interfaces.IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<IEnumerable<CurrentWeatherDTO>> GetCurrentWeather(CurrentQueryDTO queryDTO)
        {
            _configuration.SetDefaultForecastApi(queryDTO.ForecastApi);
            string cities = string.Join(",", queryDTO.Cities);
            var weatherCommand = new WeatherCommand(cities, _configuration, 0);
            var currentWeatherList = await weatherCommand.GetResultAsync();
            List<CurrentWeatherDTO> dtoList = currentWeatherList.Select(p => _mapper.Map<CurrentWeatherDTO>(p.Model)).ToList();
            return dtoList;
        }
    }
}
