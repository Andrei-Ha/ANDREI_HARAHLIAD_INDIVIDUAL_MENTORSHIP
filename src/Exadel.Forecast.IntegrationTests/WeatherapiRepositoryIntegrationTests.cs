using Exadel.Forecast.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.IntegrationTests
{
    public class WeatherapiRepositoryIntegrationTests
    {
        [Theory]
        [InlineData("Pinsk")]
        [InlineData("Kiev")]
        public void GetTempByName_ForCityName_ReturnTemperatureIsMoreThanMinus273(string city)
        {
            // Arrange
            var repositoru = new WeatherapiRepository(Environment.GetEnvironmentVariable("WEATHERAPI_API_KEY"));

            // Act
            var currentResponseModel = repositoru.GetTempByNameAsync(city);

            // Assert
            Assert.True(currentResponseModel.Result.Temperature > -273);
        }
    }
}
