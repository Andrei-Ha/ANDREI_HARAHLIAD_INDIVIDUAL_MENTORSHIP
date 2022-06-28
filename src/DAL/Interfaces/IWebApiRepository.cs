using Exadel.Forecast.DAL.Models;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IWebApiRepository
    {
        Task<ResponseModel> GetTempByName(string cityName);
        Task<ForecastResponseModel[]> GetForecastByName(string cityName);
    }
}
