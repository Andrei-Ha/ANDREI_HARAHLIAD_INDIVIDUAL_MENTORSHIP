namespace Exadel.Forecast.Api.DTO
{
    public class ForecastQueryDTO
    {
        public List<string> Cities { get; set; } = new();
        public int Days { get; set; } = 1;
    }
}
