using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Validators
{
    public class TimeIntervalValidator : IValidator<TimeInterval>
    {
        public bool IsValid(TimeInterval ti)
        {
            return ti.StartDateTime != default && ti.EndDateTime != default && ti.StartDateTime <= ti.EndDateTime;
        }
    }
}
