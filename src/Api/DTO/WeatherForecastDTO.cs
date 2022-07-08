using Exadel.Forecast.Domain.Models;

namespace Exadel.Forecast.Api.DTO
{
    public class WeatherForecastDTO
    {
        public string City { get; set; } = string.Empty;
        public List<DayModel> Days { get; set; } = new List<DayModel>();
    }
}
