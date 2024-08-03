using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Weather.Models;

namespace Weather.Repository
{
    public class LoadForecastsRepository : ILoadForecastsRepository
    {
        public WeatherDbContext _context { get; }

        private HttpClient weatherServiceClient;

        public LoadForecastsRepository(WeatherDbContext context)
        {
            _context = context;
            weatherServiceClient = new HttpClient();
            weatherServiceClient.BaseAddress = new Uri("https://api.weather.gov");
            weatherServiceClient.DefaultRequestHeaders.Add(
                "User-Agent",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X x.y; rv:42.0) Gecko/20100101 Firefox/42.0"
            );
        }

        public async Task<int> Add(string locationName)
        {
            return await LoadForLocation(locationName);
        }

        public async Task<int> AddAll()
        {
            var locationsRepository = new LocationsRepository(_context);
            var locations = await locationsRepository.GetAll();
            var itemsAdded = 0;

            foreach (var location in locations)
            {
                itemsAdded += await LoadForLocation(location.Name);
            }

            return itemsAdded;
        }

        private async Task<int> LoadForLocation(string locationName)
        {
            var dailyForecastRepository = new DailyForecastRepository(_context);
            var locationsRepository = new LocationsRepository(_context);
            var location = await locationsRepository.GetByName(locationName);

            Task<String> weatherData = CallWeatherService("points/" + location.Latitude + "," + location.Longitude);
            var jsonData = JObject.Parse(weatherData.Result);

            String forecastUrl = (String)jsonData["properties"]["forecast"];
            Task<String>? forecast = CallWeatherService(forecastUrl);
            var forecastObject = JObject.Parse(forecast.Result);

            var periods = (JArray)forecastObject["properties"]["periods"];

            foreach (JObject item in periods)
            {
                var f = new DailyForecast();
                f.LocationId = location.Id;
                f.StartTime = (DateTime)item.GetValue("startTime");
                f.EndTime = (DateTime)item.GetValue("endTime");
                f.Temperature = (int)item.GetValue("temperature");
                f.WindSpeed = (string)item.GetValue("windSpeed");
                f.WindDirection = (string)item.GetValue("windDirection");
                f.ShortForecast = (string)item.GetValue("shortForecast");
                await dailyForecastRepository.Add(f);
            }

            return periods.Count;
        }

        private async Task<String> CallWeatherService(string urlParameters)
        {
            using HttpResponseMessage response = await weatherServiceClient.GetAsync(urlParameters);
            response.EnsureSuccessStatusCode(); // Throw an exception if error
            return await response.Content.ReadAsStringAsync();
        }
    }
}
