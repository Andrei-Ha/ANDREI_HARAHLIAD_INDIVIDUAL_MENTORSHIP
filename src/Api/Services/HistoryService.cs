using AutoMapper;
using Exadel.Forecast.Api.Builders;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.Api.Strategies;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Models;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ModelsInterfaces = Exadel.Forecast.Models.Interfaces;

namespace Exadel.Forecast.Api.Services
{
    public class HistoryService : IWeatherService<WeatherHistoryDTO, HistoryQueryDTO>
    {
        public readonly WeatherDbContext _dbContext;
        public readonly IMapper _mapper;
        private readonly ModelsInterfaces.IConfiguration _configuration;
        private readonly IValidator<string> _cityValidator;
        private readonly IValidator<TimeInterval> _timeIntervalValidator;

        public HistoryService(
            WeatherDbContext dbContext,
            IMapper mapper,
            ModelsInterfaces.IConfiguration configuration,
            IValidator<string> cityValidator,
            IValidator<TimeInterval> timeIntervalValidator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
            _cityValidator = cityValidator;
            _timeIntervalValidator = timeIntervalValidator;
        }

        public async Task<IEnumerable<WeatherHistoryDTO>> Get(HistoryQueryDTO queryDTO)
        {
            var commandBuilder = new HistoryCommandApiBuilder(
                _configuration, _cityValidator, queryDTO, _timeIntervalValidator, _dbContext);
            var historyCommand = await commandBuilder.BuildCommand();
            var historyStrategy = new HistoryStrategy(_mapper);
            return await historyStrategy.Execute(historyCommand);
        }
    }
}
