using Exadel.Forecast.Api.DTO;

namespace Exadel.Forecast.Api.Interfaces
{
    public interface ICurrentService
    {
        Task<IEnumerable<CurrentWeatherDTO>> GetCurrent(CurrentQueryDTO queryDTO);
    }
}
