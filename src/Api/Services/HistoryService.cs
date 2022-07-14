using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;

namespace Exadel.Forecast.Api.Services
{
    public class HistoryService : IWeatherService<WeatherHistoryDTO, HistoryQueryDTO>
    {
        public Task<IEnumerable<WeatherHistoryDTO>> Get(HistoryQueryDTO query)
        {
            throw new NotImplementedException();
        }
    }
}
