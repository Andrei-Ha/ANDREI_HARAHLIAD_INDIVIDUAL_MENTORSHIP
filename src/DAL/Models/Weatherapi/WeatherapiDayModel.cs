using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Models.Weatherapi
{
    public class WeatherapiDayModel
    {
        public Location Location { get; set; }
        public Current Current { get; set; }
    }
}
