using Exadel.Forecast.BL;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.Models.Configuration;

namespace Exadel.Forecast.IntegrationTests
{
    public class HandlerIntegrationTests
    {
        [Theory]
        [InlineData("Minsk")]
        [InlineData("Kiev")]
        public void GetWeatherByName_ForOpenWeatherRepository_ReturnStringBeginningWithIn(string city)
        {
            // Arrange
            var configuration = new Configuration();
            configuration.OpenWeatherKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY");
            configuration.SetDefaultForecastApi(ForecastApi.OpenWeather);
            var handler = new Handler(configuration, new CityValidator(), new ResponseBuilder(new TemperatureValidator()));

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
            var configuration = new Configuration();
            configuration.WeatherApiKey = Environment.GetEnvironmentVariable("WEATHERAPI_API_KEY");
            configuration.SetDefaultForecastApi(ForecastApi.WeatherApi);
            var handler = new Handler(configuration, new CityValidator(), new ResponseBuilder(new TemperatureValidator()));

            // Act
            var result = handler.GetWeatherByName(city);

            // Assert
            Assert.StartsWith($"In {city}", result);
        }
    }
}