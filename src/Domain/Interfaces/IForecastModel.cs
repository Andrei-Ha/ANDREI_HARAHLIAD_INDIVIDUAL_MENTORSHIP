namespace Exadel.Forecast.Domain.Interfaces
{
    public interface IForecastModel
    {
        IDayForecastModel[] GetDaysForecastModel();
    }
}
