namespace Exadel.Forecast.Api.DTO
{
    public class ForecastQueryDTO : BaseQueryDTO
    {
        public int Days { get; set; } = 1;
    }
}
