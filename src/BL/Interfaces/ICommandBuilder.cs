using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.Models.Configuration;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Interfaces
{
    public interface ICommandBuilder<TCommand> where TCommand : ICommand
    {
        Task<TCommand> BuildCommand();
    }
}