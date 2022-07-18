using AutoMapper;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.BL.Interfaces;

namespace Exadel.Forecast.Api.Strategies
{
    public class HistoryStrategy : IWeatherStrategy<IEnumerable<WeatherHistoryDTO>>
    {
        private readonly IMapper _mapper;

        public HistoryStrategy(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<WeatherHistoryDTO>> Execute(ICommand weatherCommand)
        {
            var weatherForecastList = await weatherCommand.GetResultAsync();
            List<WeatherHistoryDTO> dtoList = weatherForecastList.Select(p => _mapper.Map<WeatherHistoryDTO>(p.Model)).ToList();
            return dtoList;
        }
    }
}
