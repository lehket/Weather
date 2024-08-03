using Weather.Models;

namespace Weather.Repository
{
    public interface ILocationsRepository
    {
        Task<IEnumerable<Location>> GetAll();
        Task<Location> GetById(int id);
        Task<Location> GetByName(string name);
        Task<Location> Add(Location location);
        Task<Location> Update(int id, Location location);
        Task<Location> Delete(int id);
    }
}
