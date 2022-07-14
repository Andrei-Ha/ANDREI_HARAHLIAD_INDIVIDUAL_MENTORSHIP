using Exadel.Forecast.Models.Configuration;

namespace Exadel.Forecast.Api.DTO
{
    public class CurrentQueryDTO : BaseQueryDTO
    {
        public ForecastApi ForecastApi { get; set; } = ForecastApi.OpenWeather;
    }
}
