using Exadel.Forecast.Domain.Models;

namespace Exadel.Forecast.Api.DTO
{
    public class WeatherHistoryDTO : BaseWeatherDTO
    {
        public List<CurrentModel> History { get; set; } = new List<CurrentModel>();
    }
}
