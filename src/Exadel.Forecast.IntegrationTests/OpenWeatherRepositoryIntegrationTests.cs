using Exadel.Forecast.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.IntegrationTests
{
    public class OpenWeatherRepositoryIntegrationTests
    {
        [Theory]
        [InlineData("Pinsk")]
        [InlineData("Kiev")]
        public void GetTempByName_ForCityName_ReturnTemperatureIsMoreThanMinus273(string city)
        {
            // Arrange
            var repositoru = new OpenWeatherRepository(Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY"));

            // Act
            var result = repositoru.GetTempByName(city);

            // Assert
            Assert.True(result > -273);
        }
    }
}
