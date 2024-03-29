﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.Domain.Models
{
    public class ForecastModel : BaseModel
    {
        public string City { get; set; }
        public CurrentModel Current { get; set; } = new CurrentModel();
        public List<DayModel> Days { get; set; } = new List<DayModel>();
        public List<CurrentModel> History { get; set; } = new List<CurrentModel>();
    }
}
