using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Models
{
    public class CurrentResponseModel
    { 
        public string City { get; set; } = string.Empty;
        public double Temperature { get; set; } = -273;
        public long RequestDuration { get; set; } = 0;
        public string TextException { get; set; }  = string.Empty;
    }
}
