using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace BlazorCountriesWasm.Server.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IConfiguration _config;
        public CityController(IConfiguration config)
        {
            _config = config;
        }

        public string connectionId = "Default";
        public string sqlCommand = "";
        IEnumerable<City>? cities;



        [HttpGet]
        [Route("api/city/")]
        public async Task<ActionResult<List<City>>> GetCities()
        {
            sqlCommand = "Select * From Cities";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                cities = await conn.QueryAsync<City>(sqlCommand);
            }
            return Ok(cities);
        }

        [HttpGet]
        [Route("api/city/{CityId}")]
        public async Task<ActionResult<City>> GetCityById(int CityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CityId", CityId, DbType.Int32);

            sqlCommand = $"Select * From Cities " +
                "Where CityId =  @CityId";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                var city = await conn.QueryFirstAsync<City>(sqlCommand, parameters);
                return Ok(city);
            }
        }

        [HttpGet]
        [Route("api/citiesbycountryid/{CountryId}")]
        public async Task<ActionResult<City>> GetCitiesByCountryId(int CountryId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", CountryId, DbType.Int32);

            sqlCommand = $"Select * From Cities " +
                "Where CountryId =  @CountryId";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                cities = await conn.QueryAsync<City>(sqlCommand, parameters);
            }
            return Ok(cities);
        }


        [HttpGet]
        [Route("api/cityname/{CityName}")]
        public async Task<ActionResult> CountCitiesByName(string CityName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CityName", CityName, DbType.String);

            sqlCommand = $"Select Count(*) From Cities " +
                "Where Upper(CityName) =  Upper(@CityName)";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                int duplicates = await conn.QueryFirstAsync<int>(sqlCommand, parameters);
                return Ok(duplicates);
            }
        }

        [HttpGet]
        [Route("api/cityname/{CityName}/{CityId}")]
        public async Task<ActionResult> CountCitiesByNameAndId(string CityName, int CityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CityName", CityName, DbType.String);
            parameters.Add("@CityId", CityId, DbType.Int32);

            sqlCommand = $"Select Count(*) From Cities " +
                "Where Upper(CityName) =  Upper(@CityName) " +
                "And CityId <> @CityId";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                int duplicates = await conn.QueryFirstAsync<int>(sqlCommand, parameters);
                return Ok(duplicates);
            }
        }

        [HttpPost]
        [Route("api/city/")]
        public async Task<ActionResult<List<City>>> CityInsert(City city)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CityName", city.CityName, DbType.String);

            sqlCommand = "Insert into Cities (CityName) values(@CityName)";
            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return Ok();
        }

        [HttpPut]
        [Route("api/city/{cityId}")]
        public async Task<ActionResult<List<City>>> CityUpdate(City city)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CityId", city.CityId, DbType.Int32);
            parameters.Add("CityName", city.CityName, DbType.String);

            sqlCommand =
                "Update Cities " +
                "set CityName = @CityName " +
                "Where CityId = @CityId";
            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("api/city/{CityId}")]
        public async Task<ActionResult> CityDelete(int CityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CityId", CityId, DbType.Int32);

            sqlCommand =
                "Delete From Cities " +
                "Where CityId = @CityId";
            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return Ok();
        }
    }
}