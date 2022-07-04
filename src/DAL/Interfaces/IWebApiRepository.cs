using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IWebApiRepository
    {
        Task<DebugModel<ForecastModel>> GetForecastAsync(string cityName, int amountOfDays, CancellationToken token = default);
    }
}
