using AutoMapper;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;

namespace Exadel.Forecast.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ForecastModel, WeatherForecastDTO>();
            CreateMap<ForecastModel, CurrentWeatherDTO>();
            CreateMap<ForecastModel, WeatherHistoryDTO>();
            //CreateMap<WeatherForecastDTO, ForecastModel>();
        }
    }
}
