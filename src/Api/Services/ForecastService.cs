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
        private readonly Models.Interfaces.IConfiguration _configuration;

        public ForecastService(IMapper mapper, Models.Interfaces.IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<IEnumerable<WeatherForecastDTO>> GetForecast(ForecastQueryDTO queryDTO)
        {
            _configuration.SetDefaultForecastApi(ForecastApi.WeatherBit);
            string cities = string.Join(",", queryDTO.Cities);
            var weatherCommand = new WeatherCommand(cities, _configuration, queryDTO.Days);
            var forecastList = await weatherCommand.GetResultAsync();
            List<WeatherForecastDTO> dtoList = forecastList.Select(p => _mapper.Map<WeatherForecastDTO>(p.Model)).ToList();
            return dtoList;
        }
    }
}
