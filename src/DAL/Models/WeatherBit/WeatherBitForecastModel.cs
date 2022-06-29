using System;
using System.Collections.Generic;
using System.Text;
using Exadel.Forecast.DAL.Interfaces;
using Newtonsoft.Json;

namespace Exadel.Forecast.DAL.Models.WeatherBit
{

    public class WeatherBitForecastModel : IForecastModel
    {
        public Datum[] Data { get; set; }

        [JsonProperty("City_name")]
        public string CityName{ get; set; }

        public string Lon { get; set; }
        public string Timezone { get; set; }
        public string Lat { get; set; }

        [JsonProperty("Country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("State_code")]
        public string StateCode { get; set; }

        public IDayForecastModel[] GetDaysForecastModel()
        {
            throw new NotImplementedException();
        }
    }
}
