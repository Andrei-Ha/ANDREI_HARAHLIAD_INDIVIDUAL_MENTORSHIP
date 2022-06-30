using Exadel.Forecast.Domain.Models;

namespace Exadel.Forecast.Domain.Interfaces
{
    public interface ICurrentWeatherModel
    {
        CurrentModel GetCurrentModel();
    }
}
