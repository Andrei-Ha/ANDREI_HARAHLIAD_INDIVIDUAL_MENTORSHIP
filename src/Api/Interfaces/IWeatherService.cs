using Exadel.Forecast.Api.DTO;

namespace Exadel.Forecast.Api.Interfaces
{
    public interface IWeatherService<TModel, TQuery> 
        where TModel : class
        where TQuery : class
    {
        Task<IEnumerable<TModel>> Get(TQuery query);
    }
}
