using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IWebApiRepository
    {
        Task<DebugModel<CurrentModel>> GetCurrentWeatherAsync(string cityName);
        Task<ForecastModel> GetWeatherForecastAsync(string cityName);
    }
}
