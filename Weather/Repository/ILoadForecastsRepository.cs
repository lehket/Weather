using Weather.Models;

namespace Weather.Repository
{
    public interface ILoadForecastsRepository
    {
        Task<int> Add(string locationName);
        Task<int> AddAll();
    }
}
