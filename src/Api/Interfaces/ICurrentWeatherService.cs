using Exadel.Forecast.Api.DTO;

namespace Exadel.Forecast.Api.Interfaces
{
    public interface ICurrentWeatherService
    {
        Task<IEnumerable<CurrentWeatherDTO>> GetCurrentWeather(CurrentQueryDTO queryDTO);
    }
}
