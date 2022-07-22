using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IWebApiRepository
    {
        Task<DebugModel<ForecastModel>> GetForecastAsync(string cityName, int amountOfDays, CancellationToken token = default);
        Task<DebugModel<ForecastModel>> GetHistoryAsync(string cityName, DateTime startDate, DateTime endDate, CancellationToken token = default);
    }
}
