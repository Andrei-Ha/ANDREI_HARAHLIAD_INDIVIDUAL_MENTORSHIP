using Exadel.Forecast.BL;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.Models.Configuration;

namespace Exadel.Forecast.IntegrationTests
{
    public class HandlerIntegrationTests
    {
        private readonly Configuration _configuration;

        public HandlerIntegrationTests() 
        {
            _configuration = new Configuration();
        }

        [Theory]
        [InlineData("Minsk")]
        [InlineData("Kiev")]
        public void GetWeatherByName_ForOpenWeatherRepository_ReturnStringBeginningWithIn(string city)
        {
            // Arrange
            _configuration.OpenWeatherKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY");
            _configuration.SetDefaultForecastApi(ForecastApi.OpenWeather);
            var handler = new Handler(_configuration, new CityValidator(), new ResponseBuilder(new TemperatureValidator()));

            // Act
            var result = handler.GetWeatherByName(city);

            // Assert
            Assert.StartsWith($"In {city}", result);
        }

        [Theory]
        [InlineData("Minsk")]
        [InlineData("Kiev")]
        public void GetWeatherByName_ForWeatherapiRepository_ReturnStringBeginningWithIn(string city)
        {
            // Arrange
            _configuration.WeatherApiKey = Environment.GetEnvironmentVariable("WEATHERAPI_API_KEY");
            _configuration.SetDefaultForecastApi(ForecastApi.WeatherApi);
            var handler = new Handler(_configuration, new CityValidator(), new ResponseBuilder(new TemperatureValidator()));

            // Act
            var result = handler.GetWeatherByName(city);

            // Assert
            Assert.StartsWith($"In {city}", result);
        }
    }
}