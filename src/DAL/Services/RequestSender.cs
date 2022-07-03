using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Services
{
    public class RequestSender<T>
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _webUrl;

        public RequestSender(string webUrl)
        {
            _webUrl = webUrl;
        }

        private async Task<T> Get(CancellationToken token = default)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_webUrl, token);
            response.EnsureSuccessStatusCode();
            string strModel = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(strModel);
        }

        public async Task<T> GetModelAsync()
        {
            try
            {
                return await Get();
            }
            catch (Exception ex)
            {
                // Logger(ex.Message)
                Console.WriteLine(ex.Message);
            }

            return default;
        }

        public async Task<DebugModel<T>> GetDebugModelAsync(CancellationToken token = default)
        {
            var debugModel = new DebugModel<T>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                T model = await Get(token);
                if (model != null)
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
