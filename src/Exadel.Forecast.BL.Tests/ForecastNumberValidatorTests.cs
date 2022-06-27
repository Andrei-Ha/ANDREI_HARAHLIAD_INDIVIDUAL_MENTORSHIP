using Exadel.Forecast.BL.Validators;

namespace Exadel.Forecast.BL.Tests
{
    public class ForecastNumberValidatorTests
    {
        private readonly ForecastNumberValidator _validator;

        public ForecastNumberValidatorTests()
        {
            _validator = new ForecastNumberValidator(3, 9);
        }
        
        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(9)]
        public void IaValid_ForTemperatureFromTheRange_ReturnTrue(int number) 
        {
            // Act
            var result = _validator.IsValid(number);

            // Assert
            Assert.True(result);

        }

        [Theory]
        [InlineData(-20)]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(10)]
        [InlineData(55)]
        public void IaValid_ForTemperatureOutOfTheRange_ReturnFalse(int number)
        {
            // Act
            var result = _validator.IsValid(number);

            // Assert
            Assert.False(result);

        }
    }
}
