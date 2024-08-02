using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weather.Models;
using Weather.Repository;

namespace Weather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyForecastsController : ControllerBase
    {
        private readonly IDailyForecastRepository _dailyForecastRepository;

        public DailyForecastsController(IDailyForecastRepository dailyForecastRepository)
        {
            _dailyForecastRepository = dailyForecastRepository;
        }

        // GET: api/dailyForecasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyForecast>>> GetDailyForecasts()
        {
            return (await _dailyForecastRepository.GetAll()).ToList();
        }

        // GET: api/dailyForecasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyForecast>> GetDailyForecast(int id)
        {
            var dailyForecast = await _dailyForecastRepository.GetById(id);

            if (dailyForecast == null)
            {
                return NotFound();
            }

            return dailyForecast;
        }

        // PUT: api/dailyForecasts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<DailyForecast>> PutDailyForecast(int id, DailyForecast dailyForecast)
        {
            if (id != dailyForecast.Id)
            {
                return BadRequest();
            }

            var result = await _dailyForecastRepository.Update(id, dailyForecast);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/dailyForecasts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DailyForecast>> PostDailyForecast(DailyForecast dailyForecast)
        {
            await _dailyForecastRepository.Add(dailyForecast);
            return CreatedAtAction(nameof(GetDailyForecast), new { id = dailyForecast.Id }, dailyForecast);
        }

        // DELETE: api/dailyForecasts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedailyForecast(int id)
        {
            var result = await _dailyForecastRepository.Delete(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
