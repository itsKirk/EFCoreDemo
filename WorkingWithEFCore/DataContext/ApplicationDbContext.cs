using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options){}
        public DbSet<Car> Cars { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
