using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCountriesWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        public static List<City> cities = new List<City>
        {
            new City
            {
                CityId = 1,
                CityName = "London",
                CityPopulation = 9000000,
                CountryId = 1
            },
            new City
            {
                CityId = 2,
                CityName = "Birmingham",
                CityPopulation = 1500000,
                CountryId = 1
            },
            new City
            {
                CityId = 3,
                CityName = "Oxford",
                CityPopulation = 250000,
                CountryId = 1
            },
            new City
            {
                CityId = 4,
                CityName = "Cambridge",
                CityPopulation = 200000,
                CountryId = 1
            },
            new City
            {
                CityId = 5,
                CityName = "Paris",
                CityPopulation = 7500000,
                CountryId = 2
            },
            new City
            {
                CityId = 6,
                CityName = "Toulouse",
                CityPopulation = 200000,
                CountryId = 2
            },
            new City
            {
                CityId = 7,
                CityName = "Grenoble",
                CityPopulation = 200000,
                CountryId = 2
            }
        };

        [HttpGet]
        public async Task<ActionResult<List<City>>> GetCities()
        {
            return Ok(cities);
        }

        [HttpGet]
        [Route("{CityId}")]
        //Or you can combine the two lines as: [HttpGet("{CityId}")]
        public async Task<ActionResult<City>> GetSingleCity(int CityId)
        {
            var city = cities.FirstOrDefault(c => c.CityId == CityId);
            if (city == null)
            {
                return NotFound("Sorry, no city found.");
            }
            else
            {
                return Ok(city);
            }
        }
    }
}
