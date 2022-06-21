namespace Exadel.Forecast.DAL.Models.Weatherapi
{
    public class Forecastday
    {
        public string Date { get; set; }
        public int Date_epoch { get; set; }
        public Day Day { get; set; }
    }
}
