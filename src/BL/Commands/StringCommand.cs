using Exadel.Forecast.BL.Interfaces;

using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Commands
{
    public class StringCommand : ICommand
    {
        private readonly string _string;

        public StringCommand(string @string)
        {
            _string = @string;
        }

        public Task<string> GetResult()
        {
            return Task.FromResult(_string);
        }
    }
}
