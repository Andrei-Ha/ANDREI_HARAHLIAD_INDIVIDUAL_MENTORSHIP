using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IWebApiRepository
    {
        Task<DebugModel<CurrentModel>> GetCurrentWeatherAsync(string cityName, CancellationToken token = default);
        Task<ForecastModel> GetWeatherForecastAsync(string cityName, int amountOfDays);
    }
}
