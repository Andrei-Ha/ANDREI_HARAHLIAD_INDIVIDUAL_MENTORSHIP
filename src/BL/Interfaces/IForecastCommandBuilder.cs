using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.Models.Configuration;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Interfaces
{
    public interface IForecastCommandBuilder<TCommand> where TCommand : IForecastCommand
    {
        Task<TCommand> BuildCommand();
    }
}