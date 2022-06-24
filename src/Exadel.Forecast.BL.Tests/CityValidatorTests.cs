using Exadel.Forecast.BL.Validators;

namespace Exadel.Forecast.BL.Tests
{
    public class CityValidatorTests
    {
        private readonly CityValidator _cityValidator;

        public CityValidatorTests()
        {
            _cityValidator = new CityValidator();
        }
        [Theory]
        [InlineData(default(string))]
        [InlineData("")]
        public void IsValid_ForNullOrEmpty_ReturnFalse(string value)
        {
            // Act
            var result = _cityValidator.IsValid(value);

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
            // Act
            var result = _cityValidator.IsValid(value);

            // Assert
            Assert.True(result);
        }
    }
}
