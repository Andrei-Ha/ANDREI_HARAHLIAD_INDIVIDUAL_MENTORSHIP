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
        static readonly HttpClient _httpClient = new HttpClient();
        public static async Task<T> GetModel(string webUrl)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(webUrl);
                response.EnsureSuccessStatusCode();
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    StreamReader reader = new StreamReader(responseStream);
                    //Console.WriteLine(reader.ReadToEnd());
                    return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            return default;
        }
    }
}
