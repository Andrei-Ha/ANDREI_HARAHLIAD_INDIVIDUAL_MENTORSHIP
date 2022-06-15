using Exadel.Forecast.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL.Validators
{
    public class TemperatureValidator : IValidator<double>
    {
        public bool IsValid(double value)
        {
            return value > -273;
        }
    }
}
