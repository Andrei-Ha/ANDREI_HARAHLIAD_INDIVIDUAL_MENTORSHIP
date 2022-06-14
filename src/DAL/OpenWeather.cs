using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL
{
    public static class OpenWeather
    {
        static readonly HttpClient _httpClient = new HttpClient();
        public static async Task<float> GetTemperature(string city)
        {
            float temperature = -273;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&APPID=ea0efbda4d022014db4216e464d509c8&units=metric");
                response.EnsureSuccessStatusCode();
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    StreamReader reader = new StreamReader(responseStream);
                    //Console.WriteLine(reader.ReadToEnd());
                    DayForecastModel dayForecastModel = JsonConvert.DeserializeObject<DayForecastModel>(reader.ReadToEnd());
                    temperature = dayForecastModel.main.temp;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            return temperature;
        }
    }
}
