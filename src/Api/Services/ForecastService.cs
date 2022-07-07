using AutoMapper;
using Exadel.Forecast.Api.Builders;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.Api.Strategies;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;

namespace Exadel.Forecast.Api.Services
{
    public class ForecastService : IForecastService
    {
        private readonly IMapper _mapper;
        private readonly Models.Interfaces.IConfiguration _configuration;
        private readonly IValidator<string> _cityValidator;

        public ForecastService(
            IMapper mapper,
            Models.Interfaces.IConfiguration configuration,
            IValidator<string> cityValidator)
        {
            _mapper = mapper;
            _configuration = configuration;
            _cityValidator = cityValidator;
        }

        public async Task<IEnumerable<WeatherForecastDTO>> GetForecast(ForecastQueryDTO queryDTO)
        {
            var forecastNumberValidator = new ForecastNumberValidator(_configuration.MinAmountOfDays, _configuration.MaxAmountOfDays);
            var commandBuilder = new ForecastCommandApiBuilder(_configuration, _cityValidator, queryDTO, forecastNumberValidator);
            var weatherCommand = await commandBuilder.BuildCommand();
            var forecastStrategy = new ForecastStrategy(_mapper);
            return await forecastStrategy.Execute(weatherCommand);
        }
    }
}
