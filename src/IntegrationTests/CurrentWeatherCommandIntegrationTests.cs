using Exadel.Forecast.BL;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.Models.Configuration;

namespace Exadel.Forecast.IntegrationTests
{
    public class CurrentWeatherCommandIntegrationTests
    {
        private readonly Configuration _configuration;

        public CurrentWeatherCommandIntegrationTests() 
        {
            _configuration = new Configuration();
        }

        [Theory]
        [InlineData("Minsk")]
        [InlineData("Kiev")]
        public void GetResult_ForOpenWeatherRepository_ReturnStringBeginningWithIn(string city)
        {
            // Arrange
            _configuration.OpenWeatherKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY");
            _configuration.SetDefaultForecastApi(ForecastApi.OpenWeather);
            var command = new CurrentWeatherCommand(
                city, _configuration, new CityValidator(), new TemperatureValidator(), new ResponseBuilder());

            // Act
            var result = command.GetResultAsync();

            // Assert
            Assert.StartsWith($"In {city}", result.Result);
        }

        [Theory]
        [InlineData("Minsk")]
        [InlineData("Kiev")]
        public void GetResult_ForWeatherapiRepository_ReturnStringBeginningWithIn(string city)
        {
            // Arrange
            _configuration.WeatherApiKey = Environment.GetEnvironmentVariable("WEATHERAPI_API_KEY");
            _configuration.SetDefaultForecastApi(ForecastApi.WeatherApi);
            var command = new CurrentWeatherCommand(
                city, _configuration, new CityValidator(), new TemperatureValidator(), new ResponseBuilder());

            // Act
            var result = command.GetResultAsync();

            // Assert
            Assert.StartsWith($"In {city}", result.Result);
        }
    }
}