using Exadel.Forecast.Models.Configuration;

namespace Exadel.Forecast.Api.DTO
{
    public class CurrentQueryDTO
    {
        public List<string> Cities { get; set; } = new();
        public ForecastApi ForecastApi { get; set; } = ForecastApi.OpenWeather;
    }
}
