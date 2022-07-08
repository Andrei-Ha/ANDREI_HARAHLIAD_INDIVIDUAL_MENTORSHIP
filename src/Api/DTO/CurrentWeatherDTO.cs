using Exadel.Forecast.Domain.Models;

namespace Exadel.Forecast.Api.DTO
{
    public class CurrentWeatherDTO
    {
        public string City { get; set; } = string.Empty;
        public CurrentModel Current { get; set; } = new CurrentModel();
    }
}
