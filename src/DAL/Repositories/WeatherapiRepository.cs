using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.Weatherapi;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Repositories
{
    public class WeatherapiRepository : IWebApiRepository
    {
        private readonly string _apiKey;

        public WeatherapiRepository(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<CurrentResponseModel> GetTempByNameAsync(string cityName)
        {
            CurrentResponseModel rm = new CurrentResponseModel() { City = cityName};
            Stopwatch stopwatch = new Stopwatch();
            string webUrl = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={cityName}";
            var requestSender = new RequestSender<WeatherapiDayModel>();
            stopwatch.Start();

            try
            {
                var model = await requestSender.GetModelAsync(webUrl);

                if (model != null)
                {
                    rm.Temperature = model.Current.TempC;
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
