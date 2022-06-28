using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Interfaces
{
    public interface ICommand
    {
        Task<string> GetResultAsync();
    }
}
