using Exadel.Forecast.Domain.Models;

namespace Exadel.Forecast.Api.DTO
{
    public class CurrentWeatherDTO : BaseWeatherDTO
    {
        public CurrentModel Current { get; set; } = new CurrentModel();
    }
}
