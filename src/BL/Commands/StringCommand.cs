using Exadel.Forecast.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL.Commands
{
    public class StringCommand : ICommand
    {
        private readonly string _string;

        public StringCommand(string @string)
        {
            _string = @string;
        }

        public string GetResult()
        {
            return _string;
        }
    }
}
