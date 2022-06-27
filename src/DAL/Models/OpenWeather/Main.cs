using Newtonsoft.Json;

namespace Exadel.Forecast.DAL.Models.OpenWeather
{
    public class Main
    {
        public double Temp { get; set; }

        [JsonProperty("Temp_min")]
        public double TempMin { get; set; }

        [JsonProperty("Temp_max")]
        public double TempMax { get; set; }

        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }

}
