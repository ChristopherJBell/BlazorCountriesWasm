using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SQLite;

namespace BlazorCountriesWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IConfiguration _config;
        public CountryController(IConfiguration config)
        {
            _config = config;
        }

        public string connectionId = "Default";
        public string sqlCommand = "";
        IEnumerable<Country> countries;


        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {            
            sqlCommand = "Select * From Countries";

            //using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            using var conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                countries = await conn.QueryAsync<Country>(sqlCommand);
            }
            return Ok(countries);

        }

        [HttpGet]
        [Route("{CountryId}")]
        //Or you can combine the two lines as: [HttpGet("{CountryId}")]
        public async Task<ActionResult<Country>> GetCountryById(int CountryId)
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
