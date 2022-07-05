using Exadel.Forecast.Api.DTO;

namespace Exadel.Forecast.Api.Interfaces
{
    public interface IForecastService
    {
        Task<IEnumerable<WeatherForecastDTO>> GetForecast(ForecastQueryDTO queryDTO);
    }
}
