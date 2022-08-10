using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCountriesWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        public static List<Country> countries = new List<Country>
        {
            new Country {CountryId = 1, CountryName = "United Kingdom"},
            new Country {CountryId = 2, CountryName ="France"}
        };

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            return Ok(countries);
        }

        [HttpGet]
        [Route("{CountryId}")]
        //Or you can combine the two lines as: [HttpGet("{CountryId}")]
        public async Task<ActionResult<Country>> GetSingleCountry(int CountryId)
        {
            var country = countries.FirstOrDefault(c => c.CountryId == CountryId);
            if (country == null)
            {
                return NotFound("Sorry, no country found.");
            }
            else
            {
                return Ok(country);
            }
        }
    } 

}
