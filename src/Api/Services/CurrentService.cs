using AutoMapper;
using Exadel.Forecast.Api.Builders;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.Api.Strategies;
using Exadel.Forecast.BL.Interfaces;
using ModelsInterfaces = Exadel.Forecast.Models.Interfaces;

namespace Exadel.Forecast.Api.Services
{
    public class CurrentService : ICurrentService
    {
        private readonly IMapper _mapper;
        private readonly ModelsInterfaces.IConfiguration _configuration;
        private readonly IValidator<string> _cityValidator;

        public CurrentService(IMapper mapper, ModelsInterfaces.IConfiguration configuration, IValidator<string> cityValidator)
        {
            _mapper = mapper;
            _configuration = configuration;
            _cityValidator = cityValidator;
        }

        public async Task<IEnumerable<CurrentWeatherDTO>> GetCurrent(CurrentQueryDTO queryDTO)
        {
            var commandBuilder = new CurrentCommandApiBuilder(_configuration, _cityValidator, queryDTO);
            var weatherCommand = await commandBuilder.BuildCommand();
            var currentStrategy = new CurrentStrategy(_mapper);
            return await currentStrategy.Execute(weatherCommand);
        }
    }
}
