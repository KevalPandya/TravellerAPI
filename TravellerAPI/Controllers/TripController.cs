using Microsoft.AspNetCore.Mvc;
using TravellerAPI.Classes;
using TravellerAPI.Models;

namespace TravellerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController : Controller
    {
        private readonly ILogger<TripController> _logger;

        public TripController(ILogger<TripController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Trip Get(string fromCity)
        {
            var tripRoute = new List<City>();
            var city = Utils.GetCities(fromCity).FirstOrDefault();
            if (city == null)
                return null;

            tripRoute.Add(city);

            var numberOfContinents = Utils.GetCities().Select(c => c.contId).Distinct().Count();
            var continentsToIgnore = new List<string>();

            for (int i = 1; i < numberOfContinents; i++)
            {
                continentsToIgnore.Add(city.contId);
                city = Utils.GetNextNearestCity(city, continentsToIgnore);
                tripRoute.Add(city);
            }

            var distance = 0;

            for (int i = 0; i < tripRoute.Count; i++)
            {
                var sourceCity = tripRoute.ElementAt(i).location;
                var destinationCity = (i == tripRoute.Count - 1) ? tripRoute.ElementAt(0).location : tripRoute.ElementAt(i + 1).location;

                distance = Convert.ToInt32(Utils.GetDistanceInKm(sourceCity.lat, sourceCity.lon, destinationCity.lat, destinationCity.lon));
            }

            var trip = new Trip()
            {
                cities = tripRoute,
                distance = distance
            };

            return trip;
        }
    }
}