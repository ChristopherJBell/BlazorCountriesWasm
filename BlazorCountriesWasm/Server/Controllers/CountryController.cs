using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SQLite;

namespace BlazorCountriesWasm.Server.Controllers
{
    //[Route("api/[controller]")]
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
        [Route("api/country/")]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            sqlCommand = "Select * From Countries";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                countries = await conn.QueryAsync<Country>(sqlCommand);
            }
            return Ok(countries);
        }

        [HttpGet]
        [Route("api/country/{CountryId}")]        
        public async Task<ActionResult<Country>> GetCountryById(int CountryId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", CountryId, DbType.Int32);

            sqlCommand = $"Select * From Countries " +
                "Where CountryId =  @CountryId";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                var country = await conn.QueryFirstAsync<Country>(sqlCommand, parameters);
                return Ok(country);
            }
        }

        [HttpGet]
        [Route("api/countryname/{CountryName}")]
        public async Task<ActionResult> CountCountriesByName(string CountryName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryName", CountryName, DbType.String);

            sqlCommand = $"Select Count(*) From Countries " +
                "Where Upper(CountryName) =  Upper(@CountryName)";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                int duplicates = await conn.QueryFirstAsync<int>(sqlCommand, parameters);
                return Ok(duplicates);
            }
        }

        [HttpGet]
        [Route("api/countryname/{CountryName}/{CountryId}")]
        public async Task<ActionResult> CountCountriesByNameAndId(string CountryName, int CountryId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryName", CountryName, DbType.String);
            parameters.Add("@CountryId", CountryId, DbType.Int32);

            sqlCommand = $"Select Count(*) From Countries " +
                "Where Upper(CountryName) =  Upper(@CountryName) " +
                "And CountryId <> @CountryId";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                int duplicates = await conn.QueryFirstAsync<int>(sqlCommand, parameters);
                return Ok(duplicates);
            }
        }

        [HttpPost]
        [Route("api/country/")]
        public async Task<ActionResult<List<Country>>> CountryInsert(Country country)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CountryName", country.CountryName, DbType.String);

            sqlCommand = "Insert into Countries (CountryName) values(@CountryName)";
            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return Ok();
        }

        [HttpPut]
        [Route("api/country/{countryId}")]
        public async Task<ActionResult<List<Country>>> CountryUpdate(Country country)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CountryId", country.CountryId, DbType.Int32);
            parameters.Add("CountryName", country.CountryName, DbType.String);

            sqlCommand =
                "Update Countries " +
                "set CountryName = @CountryName " +
                "Where CountryId = @CountryId";
            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("api/country/{CountryId}")]
        public async Task<ActionResult> CountryDelete(int CountryId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CountryId", CountryId, DbType.Int32);

            sqlCommand =
                "Delete From Countries " +
                "Where CountryId = @CountryId";
            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return Ok();
        }
    }
}