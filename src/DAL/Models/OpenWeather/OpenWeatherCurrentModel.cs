using Exadel.Forecast.Domain;
using Exadel.Forecast.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Models.OpenWeather
{
    public class OpenWeatherCurrentModel : ICurrentWeatherModel
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

        public CurrentModel GetCurrentModel()
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(Dt);
            return new CurrentModel() { City = Name, Temperature = Main.Temp, Date = dateTime };
        }
    }

}
