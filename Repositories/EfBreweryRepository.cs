using BreweryApi.Data;
using BreweryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Repositories
{
    public class EfBreweryRepository : IBreweryRepository
    {
        private readonly BreweryDbContext _context;

        public EfBreweryRepository(BreweryDbContext context)
        {
            _context = context;
        }

        public async Task SaveAllAsync(IEnumerable<BreweryModel> breweries)
        {
            _context.Breweries.RemoveRange(_context.Breweries);
            await _context.Breweries.AddRangeAsync(breweries);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BreweryModel>> GetAllAsync()
        {
            return await _context.Breweries.ToListAsync();
        }

        public async Task<BreweryModel?> GetByIdAsync(string id)
        {
            return await _context.Breweries.FindAsync(id);
        }

        public async Task AddAsync(BreweryModel brewery)
        {
            _context.Breweries.Add(brewery);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BreweryModel brewery)
        {
            _context.Breweries.Update(brewery);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var brewery = await _context.Breweries.FindAsync(id);
            if (brewery != null)
            {
                _context.Breweries.Remove(brewery);
                await _context.SaveChangesAsync();
            }
        }
    }
}
