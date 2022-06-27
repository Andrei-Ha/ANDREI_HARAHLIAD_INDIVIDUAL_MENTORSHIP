using Newtonsoft.Json;

namespace Exadel.Forecast.DAL.Models.Weatherapi
{
    public class Current
    {
        [JsonProperty("Temp_c")]
        public double TempC { get; set; }

        public Condition Condition { get; set; }
        public int Humidity { get; set; }
        public int Cloud { get; set; }
        public double Uv { get; set; }
    }

}
