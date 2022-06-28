using Exadel.Forecast.DAL.Models;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IWebApiRepository
    {
        Task<CurrentResponseModel> GetTempByNameAsync(string cityName);
        Task<ForecastResponseModel[]> GetForecastByNameAsync(string cityName);
    }
}
