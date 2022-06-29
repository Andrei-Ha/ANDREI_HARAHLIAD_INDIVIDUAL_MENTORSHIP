using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL
{
    public class RequestSender<T>
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _webUrl;

        public RequestSender(string webUrl)
        {
            _webUrl = webUrl;
        }

        public async Task<T> GetModelAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_webUrl);
            response.EnsureSuccessStatusCode();
            string strModel = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(strModel);
        }

        public async Task<DebugModel<T>> GetDebugModelAsync()
        {
            var debugModel = new DebugModel<T>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                T model = await GetModelAsync();
                if(model != null)
                {
                    debugModel.Model = model;
                }
            }
            catch (Exception e)
            {
                debugModel.TextException = e.Message;
            }

            stopwatch.Stop();
            debugModel.RequestDuration = stopwatch.ElapsedMilliseconds;
            return debugModel;
        }
    }
}
