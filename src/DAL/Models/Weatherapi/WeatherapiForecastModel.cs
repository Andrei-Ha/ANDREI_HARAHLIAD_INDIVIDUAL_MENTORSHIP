using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Models.Weatherapi
{
    public class WeatherapiForecastModel
    {
        public Location Location { get; set; }
        public Current Current { get; set; }
        public Forecast Forecast { get; set; }
    }
}
