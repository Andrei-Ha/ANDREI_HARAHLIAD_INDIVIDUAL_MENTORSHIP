using Exadel.Forecast.BL.Validators;

namespace Exadel.Forecast.BL.Tests
{
    public class TemperatureValidatorTests
    {
        private readonly TemperatureValidator _temperatureValidator;

        public TemperatureValidatorTests()
        {
            _temperatureValidator = new TemperatureValidator();
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(-273)]
        [InlineData(-312.21)]
        [InlineData(-4000)]
        public void IsValid_ForUnrealTemperature_ReturnFalse(double value)
        {
            // Act
            var result = _temperatureValidator.IsValid(value);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-272.999)]
        [InlineData(312.21)]
        [InlineData(4000)]
        [InlineData(double.MaxValue)]
        public void IsValid_ForRealTemperature_ReturnTrue(double value)
        {
            // Act
            var result = _temperatureValidator.IsValid(value);

            // Assert
            Assert.True(result);
        }
    }
}
