using AutoMapper;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.BL.Interfaces;

namespace Exadel.Forecast.Api.Strategies
{
    public class CurrentStrategy : IWeatherStrategy<IEnumerable<CurrentWeatherDTO>>
    {
        private readonly IMapper _mapper;

        public CurrentStrategy(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<CurrentWeatherDTO>> Execute(IForecastCommand weatherCommand)
        {
            var currentWeatherList = await weatherCommand.GetResultAsync();
            List<CurrentWeatherDTO> dtoList = currentWeatherList.Select(p => _mapper.Map<CurrentWeatherDTO>(p.Model)).ToList();
            return dtoList;
        }
    }
}
