using AutoMapper;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.BL.Interfaces;

namespace Exadel.Forecast.Api.Strategies
{
    public class ForecastStrategy
    {
        private readonly IMapper _mapper;

        public ForecastStrategy(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<WeatherForecastDTO>> Execute(ICommand weatherCommand)
        {
            var weatherForecastList = await weatherCommand.GetResultAsync();
            List<WeatherForecastDTO> dtoList = weatherForecastList.Select(p => _mapper.Map<WeatherForecastDTO>(p.Model)).ToList();
            return dtoList;
        }
    }
}
