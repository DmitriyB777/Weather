using Microsoft.EntityFrameworkCore;
using Weather.Entities;

namespace Weather.Context
{
    public class WeatherContext : DbContext
    {
        public DbSet<Entities.Weather> Weathers { get; set; }
        public DbSet<Trash> Trashes { get; set; }

        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) {  }
    }
}
