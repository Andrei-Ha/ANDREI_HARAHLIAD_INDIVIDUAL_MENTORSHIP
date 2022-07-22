using Newtonsoft.Json;

namespace Exadel.Forecast.DAL.Models.Weatherapi
{
    public class Forecastday
    {
        [JsonProperty("date_epoch")]
        public int DateEpoch { get; set; }
        public Day Day { get; set; }
        public Hour[] hour { get; set; }
    }
}
