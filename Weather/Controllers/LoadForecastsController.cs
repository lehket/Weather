using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Weather.Models;
using Weather.Repository;

namespace Weather.Controllers
{
    public class LoadForecastsController : Controller
    {
        private readonly ILoadForecastsRepository _loadForecastsRepository;

        public LoadForecastsController(ILoadForecastsRepository loadForecastsRepository)
        {
            _loadForecastsRepository = loadForecastsRepository;
        }


        // PUT: api/loadForecasts/Baltimore (updates specified location)
        // PUT: api/loadForecasts/all (updates all locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{locationName}")]
        public async Task<ActionResult<int>> LoadForecasts(string locationName)
        {
            if (locationName.ToLower() == "all")
            {
                return await _loadForecastsRepository.AddAll();
            }
            else
            {
                return await _loadForecastsRepository.Add(locationName);
            }
        }
    }
}
