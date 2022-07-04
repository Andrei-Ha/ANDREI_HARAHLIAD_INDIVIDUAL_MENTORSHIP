using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.Models.Configuration;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Interfaces
{
    //public interface ICommandBuilder<TCommand> where TCommand : ICommand
    //{
    //    Task<TCommand> BuildCommand();
    //}

    public interface ICommandBuilder
    {
        void Reset();
        void SetWeatherProvider(ForecastApi weatherProvider);
        void SetWeatherProviderByUser();
        void SetCityName(string cityName);
        void SetCityNameByUser();
        void SetNumberOfForecastDays(int amountOfDays);
        void SetNumberOfForecastDaysByUser();
        Task<WeatherCommand> BuildCommand();
    }
}