namespace Exadel.Forecast.DAL.Models.Weatherapi
{
    public class Day
    {
        public double Maxtemp_c { get; set; }
        public double Mintemp_c { get; set; }
        public double Avgtemp_f { get; set; }
        public double Maxwind_kph { get; set; }
        public double Totalprecip_mm { get; set; }
        public double Avgvis_km { get; set; }
        public double Avghumidity { get; set; }
        public int Daily_will_it_rain { get; set; }
        public int Daily_chance_of_rain { get; set; }
        public int Daily_will_it_snow { get; set; }
        public int Daily_chance_of_snow { get; set; }
    }
}
