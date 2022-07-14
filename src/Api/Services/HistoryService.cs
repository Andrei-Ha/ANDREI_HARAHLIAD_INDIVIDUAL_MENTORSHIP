using AutoMapper;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Exadel.Forecast.Api.Services
{
    public class HistoryService : IWeatherService<WeatherHistoryDTO, HistoryQueryDTO>
    {
        public readonly WeatherDbContext _dbContext;
        public readonly IMapper _mapper;

        public HistoryService(WeatherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WeatherHistoryDTO>> Get(HistoryQueryDTO query)
        {
            var models = _dbContext.ForecastModels.Include(p => p.History).AsNoTracking();

            if (query.Cities.Count > 0)
            {
                models = models.Where(m => query.Cities.Contains(m.City));
            }

            models = models.Select(p => new ForecastModel() {
                City = p.City, History = p.History.Where(h => h.Date >= query.StartDateTime && h.Date <= query.EndDateTime).ToList()});

            var modelsList = await models.ToListAsync();

            return modelsList.Select(p => _mapper.Map<WeatherHistoryDTO>(p)).ToList();
        }
    }
}
