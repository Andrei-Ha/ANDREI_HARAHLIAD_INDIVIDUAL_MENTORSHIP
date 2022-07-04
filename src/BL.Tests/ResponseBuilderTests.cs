using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Services;
using Moq;
namespace Exadel.Forecast.BL.Tests
{
    public class ResponseBuilderTests
    {
        private ResponseBuilder _responseBuilder;

        public ResponseBuilderTests()
        {
            _responseBuilder = new ResponseBuilder();
        }

        [Theory]
        [InlineData("London", -20)]
        [InlineData("Brest", -0.01)]
        [InlineData("Tashkent", -40)]
        public void WeatherStringByTemp_ForLessThanZero_ReturnDressWarmly(string city, double temperature)
        {
            // Act
            var result = _responseBuilder.WeatherStringByTemp(city, temperature);

            // Assert
            Assert.Equal($"In {city} {temperature} °C. Dress warmly", result);
        }

        [Theory]
        [InlineData("London", 0)]
        [InlineData("Brest", 0.01)]
        [InlineData("Tashkent", 19.99)]
        public void WeatherStringByTemp_From0To19_ReturnFresh(string city, double temperature)
        {
            // Act
            var result = _responseBuilder.WeatherStringByTemp(city, temperature);

            // Assert
            Assert.Equal($"In {city} {temperature} °C. It's fresh", result);
        }

        [Theory]
        [InlineData("London", 20)]
        [InlineData("Brest", 25)]
        [InlineData("Tashkent", 29.99)]
        public void WeatherStringByTemp_From20To30_ReturnGoodWeather(string city, double temperature)
        {
            // Act
            var result = _responseBuilder.WeatherStringByTemp(city, temperature);

            // Assert
            Assert.Equal($"In {city} {temperature} °C. Good weather", result);
        }

        [Theory]
        [InlineData("London", 30)]
        [InlineData("Brest", 100)]
        [InlineData("Tashkent", double.MaxValue)]
        public void WeatherStringByTemp_ForAboveThan30_ReturnGoToTheBeach(string city, double temperature)
        {
            // Act
            var result = _responseBuilder.WeatherStringByTemp(city, temperature);

            // Assert
            Assert.Equal($"In {city} {temperature} °C. It's time to go to the beach", result);
        }
    }
}