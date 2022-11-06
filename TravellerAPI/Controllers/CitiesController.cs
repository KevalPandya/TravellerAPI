using Microsoft.AspNetCore.Mvc;
using TravellerAPI.Classes;
using TravellerAPI.Models;

namespace TravellerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<City> Get(string cityName)
        {
            return Utils.GetCities(cityName, ExactMatch: false);
        }
    }
}