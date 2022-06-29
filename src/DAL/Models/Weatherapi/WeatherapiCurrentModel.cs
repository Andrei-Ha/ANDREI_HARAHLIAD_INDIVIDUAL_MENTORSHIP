using Exadel.Forecast.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Models.Weatherapi
{
    public class WeatherapiCurrentModel : ICurrentForecastModel
    {
        public Location Location { get; set; }
        public Current Current { get; set; }

        public CurrentModel GetCurrentModel()
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(Location.LocaltimeEpoch);
            return new CurrentModel() { City = Location.Name, Temperature = Current.TempC, Date = dateTime };
        }
    }
}
