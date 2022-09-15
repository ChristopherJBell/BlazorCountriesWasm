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

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                countries = await conn.QueryAsync<Country>(sqlCommand);
            }
            return Ok(countries);

        }

        [HttpGet("{CountryId}")]
        //[Route("{CountryId}")]
        //Or you can combine the two lines as: [HttpGet("{CountryId}")]
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

        [HttpPost]
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

        [HttpPut("{CountryId}")]
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

        [HttpDelete("{CountryId}")]
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