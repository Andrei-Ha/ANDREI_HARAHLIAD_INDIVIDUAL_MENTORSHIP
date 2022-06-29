using Newtonsoft.Json;

namespace Exadel.Forecast.DAL.Models.WeatherBit
{
    public class Datum
    {
        public int Rh { get; set; }
        public double Pres { get; set; }

        [JsonProperty("Max_temp")]
        public double MaxTemp { get; set; }

        public string DateTime { get; set; }
        public double Temp { get; set; }

        [JsonProperty("Min_temp")]
        public double MinTemp { get; set; }
        
        [JsonProperty("city_name")]
        public string CityName { get; set; }

        [JsonProperty("ts")]
        public int LocaltimeEpoch { get; set; }
    }
}
