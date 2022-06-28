using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Models
{
    public class ForecastResponseModel
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public DateTime Date { get; set; }
    }
}
