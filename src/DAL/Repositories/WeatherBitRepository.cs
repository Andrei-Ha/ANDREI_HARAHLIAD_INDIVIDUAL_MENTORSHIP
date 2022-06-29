using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.WeatherBit;
using Exadel.Forecast.Domain;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Repositories
{
    public class WeatherBitRepository : IWebApiRepository
    {
        private readonly string _apiKey;

        public WeatherBitRepository(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<CurrentModel[]> GetForecastByNameAsync(string cityName)
        {
            string webUrl = $"https://api.weatherbit.io/v2.0/forecast/daily?key={_apiKey}&city={cityName}";
            var requestSender = new RequestSender<WeatherBitForecastModel>(webUrl);
            var model = await requestSender.GetModelAsync();
            if (model != null)
            {
                return model.Data
                    .Select(p => new CurrentModel()
                        { 
                            City = cityName,
                            Temperature = p.MaxTemp,
                            Date = DateTime.ParseExact(p.DateTime, "yyyy-MM-dd", CultureInfo.InvariantCulture) 
                        })
                    .ToArray();
            }

            return null;
        }

        public Task<DebugModel<CurrentModel>> GetTempByNameAsync(string cityName)
        {
            throw new NotImplementedException();
        }
    }
}
