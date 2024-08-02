using Microsoft.EntityFrameworkCore;

namespace Weather.Models
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) 
        {
        }

        public DbSet<Location> Locations { get; set; }

        public DbSet<DailyForecast> DailyForecasts { get; set; } 

    }
}
