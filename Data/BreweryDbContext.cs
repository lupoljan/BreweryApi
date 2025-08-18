using BreweryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Data
{
    public class BreweryDbContext : DbContext
    {
        public BreweryDbContext(DbContextOptions<BreweryDbContext> options)
            : base(options) { }

        public DbSet<BreweryModel> Breweries { get; set; }
    }
}
