using Exadel.Forecast.DAL.Models;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IWebApiRepository
    {
        Task<DebugModel<CurrentModel>> GetTempByNameAsync(string cityName);
        Task<CurrentModel[]> GetForecastByNameAsync(string cityName);
    }
}
