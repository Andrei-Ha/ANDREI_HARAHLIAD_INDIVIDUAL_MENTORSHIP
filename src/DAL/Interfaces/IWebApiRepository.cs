using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IWebApiRepository
    {
        Task<DebugModel<CurrentModel>> GetTempByNameAsync(string cityName);
        Task<ForecastModel> GetForecastByNameAsync(string cityName);
    }
}
