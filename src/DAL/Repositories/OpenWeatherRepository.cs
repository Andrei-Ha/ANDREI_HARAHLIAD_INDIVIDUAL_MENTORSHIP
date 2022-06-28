using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.OpenWeather;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Repositories
{
    public class OpenWeatherRepository : IWebApiRepository
    {
        private readonly string _apiKey;

        public OpenWeatherRepository(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<CurrentResponseModel> GetTempByNameAsync(string cityName)
        {
            CurrentResponseModel rm = new CurrentResponseModel() { City = cityName};
            Stopwatch stopwatch = new Stopwatch();
            string webUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&APPID={_apiKey}&units=metric";
            var requestSender = new RequestSender<OpenWeatherDayModel>();
            stopwatch.Start();

            try
            {
                var model = await requestSender.GetModelAsync(webUrl);
                
                if (model != null)
                {
                    rm.Temperature = model.Main.Temp;
                }
            }
            catch (Exception e)
            {
                rm.TextException = e.Message;
            }

            stopwatch.Stop();
            rm.RequestDuration = stopwatch.ElapsedMilliseconds;
            return rm;
        }

        public Task<ResponseModel[]> GetForecastByNameAsync(string cityName)
        {
            throw new NotImplementedException();
        }
    }
}
