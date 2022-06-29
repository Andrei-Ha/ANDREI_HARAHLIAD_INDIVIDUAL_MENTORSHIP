using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.Domain
{
    public class CurrentModel : BaseLocationModel
    {
        public double Temperature { get; set; }
        public DateTime Date { get; set; }
    }
}
