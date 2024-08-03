using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Weather.Models;

namespace Weather.Repository
{
    public class DailyForecastRepository : IDailyForecastRepository
    {
        public WeatherDbContext _context { get; }

        public DailyForecastRepository(WeatherDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DailyForecast>> GetAll()
        {
            return await _context.DailyForecasts.ToListAsync();
        }

        public async Task<DailyForecast> GetById(int id)
        {
            return await _context.DailyForecasts.FindAsync(id);
        }

        public async Task<DailyForecast> Add(DailyForecast forecast)
        {
            var result = _context.DailyForecasts.Add(forecast);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<DailyForecast> Update(int id, DailyForecast forecast)
        {
            _context.Entry(forecast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var findForecast = await GetById(id);

                if (findForecast== null)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return forecast;
        }

        public async Task<DailyForecast> Delete(int id)
        {
            var forecast = await GetById(id);

            if (forecast == null)
            {
                return null;
            }

            _context.DailyForecasts.Remove(forecast);
            await _context.SaveChangesAsync();
            return forecast;
        }
    }
}
