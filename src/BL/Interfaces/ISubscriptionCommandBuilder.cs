using Exadel.Forecast.BL.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Interfaces
{
    public interface ISubscriptionCommandBuilder
    {
        Task<SubscriptionCommand> BuildCommand();
    }
}
