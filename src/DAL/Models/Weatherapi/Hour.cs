using Newtonsoft.Json;

namespace Exadel.Forecast.DAL.Models.Weatherapi
{
    public class Hour
    {
        [JsonProperty("time_epoch")]
        public int DateEpoch { get; set; }

        [JsonProperty("temp_c")]
        public double TempC { get; set; }
    }
}
