using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.Domain
{
    public class ForecastModel : BaseModel
    {
        public List<DayModel> Days { get; set; }
    }
}
