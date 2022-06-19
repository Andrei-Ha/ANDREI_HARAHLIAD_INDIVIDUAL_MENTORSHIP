using Exadel.Forecast.BL.Validators;

namespace Exadel.Forecast.BL.Tests
{
    public class CityValidatorTests
    {
        [Theory]
        [InlineData(default(string))]
        [InlineData("")]
        public void IsValid_ForNullOrEmpty_ReturnFalse(string value)
        {
            // Arrange
            var validator = new CityValidator();

            // Act
            var result = validator.IsValid(value);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("b")]
        [InlineData("London")]
        [InlineData("Tashkent")]
        public void IsValid_ForNotEmptyString_ReturnTrue(string value)
        {
            // Arrange
            var validator = new CityValidator();

            // Act
            var result = validator.IsValid(value);

            // Assert
            Assert.True(result);
        }
    }
}
