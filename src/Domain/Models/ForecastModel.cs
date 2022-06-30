using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.Domain.Models
{
    public class ForecastModel : BaseLocationModel
    {
        public List<DayModel> Days { get; set; }
    }
}
