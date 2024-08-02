using Microsoft.EntityFrameworkCore;
using Weather.Models;

namespace Weather.Repository
{
    public class LocationsRepository : ILocationsRepository
    {
        public WeatherDbContext _context { get; }

        public LocationsRepository(WeatherDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location> GetById(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task<Location> Add(Location location)
        {
            var result = _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Location> Update(int id, Location location)
        {
            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var findLocation = await GetById(id);

                if (findLocation == null)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return location;
        }

        public async Task<Location> Delete(int id)
        {
            var location = await GetById(id);

            if (location == null)
            {
                return null;
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return location;
        }
    }
}
