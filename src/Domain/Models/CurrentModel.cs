using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.Domain.Models
{
    public class CurrentModel
    {
        public int Id { get; set; }
        public double Temperature { get; set; }
        public DateTime Date { get; set; }
    }
}
