using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Interfaces;
using Moq;

namespace Exadel.Forecast.BL.Tests
{
    public class HandlerTests
    {
        [Theory]
        [InlineData("London", -30)]
        [InlineData("Paris", -1)]
        [InlineData("Berlin", 0)]
        public void GetWeatherByName_ForCityName_ReturnActualWeather(string city, double temperature)
        {
            //Arrange
            var mockValidator = new Mock<IValidator<string>>();
            mockValidator.Setup(x => x.IsValid(city)).Returns(true);

            var mockResponseBuilder = new Mock<IResponseBuilder>();
            mockResponseBuilder.Setup(b => b.WeatherStringByTemp(It.IsAny<string>(), It.IsAny<double>())).Returns($"In {city} {temperature} °C.It's fresh");

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetDefaultForecastApi().GetTempByName(city)).Returns(It.IsAny<double>());

            var handler = new Handler(mockConfiguration.Object, mockValidator.Object, mockResponseBuilder.Object);

            //Act
            var result = handler.GetWeatherByName(city);

            //Assert
            Assert.Equal($"In {city} {temperature} °C.It's fresh", result);
        }
    }
}
