using Exadel.Forecast.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.EF
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {

        }

        public DbSet<ForecastModel> ForecastModels { get; set; }
    }
}
