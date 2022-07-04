using Exadel.Forecast.Domain.Interfaces;
using Exadel.Forecast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Models.OpenWeather
{
    public class OpenWeatherCurrentModel : IForecastModel
    {
        public Coord Coord { get; set; }
        public Weather[] Weather { get; set; }
        public string Base { get; set; }
        public Main Main { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public int Dt { get; set; }
        public Sys Sys { get; set; }
        public int Timezone { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }

        public ForecastModel UpdateForecastModel(ForecastModel forecastModel)
        {
            forecastModel.City = Name;
            forecastModel.Current.Temperature = Main.Temp;
            forecastModel.Current.Date = DateTimeOffset.FromUnixTimeSeconds(Dt).DateTime;

            return forecastModel;
        }
    }

}
