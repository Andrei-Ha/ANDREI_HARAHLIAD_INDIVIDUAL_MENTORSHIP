﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.Domain.Models
{
    public class CurrentModel : BaseModel
    {
        public double Temperature { get; set; }
        public DateTime Date { get; set; }
    }
}
