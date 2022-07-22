using Exadel.Forecast.BL.CommandBuilders;
using Exadel.Forecast.BL.Interfaces;
using ModelsConfiguration = Exadel.Forecast.Models.Interfaces;

namespace Exadel.Forecast.Api.Builders
{
    public abstract class BaseCommandApiBuilder<TCommand> : BaseCommandBuilder<TCommand>
        where TCommand : BL.Interfaces.ICommand
    {
        const string wrongCityName = "An invalid city name was entered!";

        public BaseCommandApiBuilder(
            ModelsConfiguration.IConfiguration configuration,
            IValidator<string> cityValidator) : base(configuration, cityValidator) { }

        public override void SetCityName(string cityName)
        {
            if (!CityValidator.IsValid(CityName = cityName))
            {
                throw new HttpRequestException(wrongCityName, null, System.Net.HttpStatusCode.UnprocessableEntity);
            };
        }

        public override void SetCityName(IEnumerable<string> cityNames)
        {
            string input = string.Join(",", cityNames);

            if (!CityValidator.IsValid(input))
            {
                throw new HttpRequestException(wrongCityName, null, System.Net.HttpStatusCode.UnprocessableEntity);
            }
            else
            {
                CityName = string.Join(",", cityNames);
            }
        }

        public override abstract Task<TCommand> BuildCommand();
    }
}
