using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Models.Interfaces;
using Moq;

namespace Exadel.Forecast.BL.Tests
{
    public class CurrentWeatherCommandTests
    {
        [Theory]
        [InlineData("London", -30)]
        [InlineData("Paris", -1)]
        [InlineData("Berlin", 0)]
        public void GetWeatherByName_ForCityName_ReturnActualWeather(string city, double temperature)
        {
            //Arrange
            var mockCityValidator = new Mock<IValidator<string>>();
            mockCityValidator.Setup(x => x.IsValid(city)).Returns(true);

            var mockTempValidator = new Mock<IValidator<double>>(); 
            mockTempValidator.Setup(x => x.IsValid(temperature)).Returns(true);

            var mockResponseBuilder = new Mock<IResponseBuilder>();
            mockResponseBuilder.Setup(
                b => b.WeatherStringByTemp(new DebugModel())).Returns($"In {city} {temperature} °C.It's fresh");

            var currentResponseModel = new DebugModel() { Temperature = temperature };

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetDefaultForecastApi().GetTempByNameAsync(city).Result).Returns(currentResponseModel);

            var command = new CurrentWeatherCommand
                (
                    city, mockConfiguration.Object, mockCityValidator.Object, mockTempValidator.Object, mockResponseBuilder.Object
                );

            //Act
            var result = command.GetResultAsync().Result;

            //Assert
            Assert.Equal($"In {city} {temperature} °C.It's fresh", result);
        }
    }
}
