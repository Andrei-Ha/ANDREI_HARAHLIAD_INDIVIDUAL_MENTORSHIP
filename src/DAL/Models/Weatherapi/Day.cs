using Newtonsoft.Json;

namespace Exadel.Forecast.DAL.Models.Weatherapi
{
    public class Day
    {
        [JsonProperty("avgtemp_c")]
        public double AvgTempC { get; set; }

        [JsonProperty("Maxtemp_c")]
        public double MaxTempC { get; set; }

        [JsonProperty("Mintemp_c")]
        public double MinTempC { get; set; }
    }
}
