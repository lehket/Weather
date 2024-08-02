using Weather.Models;

namespace Weather.Repository
{
    public interface IDailyForecastRepository
    {
        Task<IEnumerable<DailyForecast>> GetAll();
        Task<DailyForecast> GetById(int id);
        Task<DailyForecast> Add(DailyForecast location);
        Task<DailyForecast> Update(int id, DailyForecast location);
        Task<DailyForecast> Delete(int id);
    }
}
