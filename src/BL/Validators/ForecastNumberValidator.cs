using Exadel.Forecast.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL.Validators
{
    public class ForecastNumberValidator : IValidator<int>
    {
        private readonly int _minValue;
        private readonly int _maxValue;
        public ForecastNumberValidator(int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }
        public bool IsValid(int value)
        {
            return value >= _minValue && value <= _maxValue;
        }
    }
}
