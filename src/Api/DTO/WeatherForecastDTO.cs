using Exadel.Forecast.Domain.Models;

namespace Exadel.Forecast.Api.DTO
{
    public class WeatherForecastDTO : BaseWeatherDTO
    {
        public List<DayModel> Days { get; set; } = new List<DayModel>();
    }
}
