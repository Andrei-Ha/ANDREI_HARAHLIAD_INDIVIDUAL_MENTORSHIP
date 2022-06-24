using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL
{
    public class RequestSender<T>
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<T> GetModel(string webUrl)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(webUrl);
                response.EnsureSuccessStatusCode();
                string strModel = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(strModel);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            return default;
        }
    }
}
