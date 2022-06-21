using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Models.WeatherBit
{

    public class WeatherBitForecastModel
    {
        public Datum[] Data { get; set; }
        public string City_name { get; set; }
        public string Lon { get; set; }
        public string Timezone { get; set; }
        public string Lat { get; set; }
        public string Country_code { get; set; }
        public string State_code { get; set; }
    }
}
