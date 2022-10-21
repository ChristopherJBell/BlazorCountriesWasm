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
        [Route("api/city/{CountryId}/{CityName}")]
        public async Task<ActionResult> CountCitiesForInsert(int CountryId, string CityName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", CountryId, DbType.Int32);
            parameters.Add("@CityName", CityName, DbType.String);

            sqlCommand = $"Select Count(*) From Cities " +
                "Where Upper(CityName) =  Upper(@CityName)" +
                " and CountryId = @CountryId";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {                
                int duplicates = await conn.QuerySingleAsync<int>(sqlCommand, parameters);
                return Ok(duplicates);
            }
        }

        [HttpGet]
        [Route("api/city/{CountryId}/{CityName}/{CityId}")]
        public async Task<ActionResult> CountCitiesForEdit(int CountryId, string CityName, int CityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", CountryId, DbType.Int32);
            parameters.Add("@CityName", CityName, DbType.String);
            parameters.Add("@CityId", CityId, DbType.Int32);

            sqlCommand = $"Select Count(*) From Cities " +
                "Where Upper(CityName) =  Upper(@CityName)" +
                " and CountryId = @CountryId" + 
                " and CityId <> @CityId";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                int duplicates = await conn.QuerySingleAsync<int>(sqlCommand, parameters);
                return Ok(duplicates);
            }
        }

        [HttpPost]
        [Route("api/city/")]
        public async Task<ActionResult<List<City>>> CityInsert(City city)
        {
            var parameters = new DynamicParameters();
            
            parameters.Add("@CityName", city.CityName, DbType.String);
            parameters.Add("@CityPopulation", city.CityPopulation, DbType.Int32);
            parameters.Add("@CountryId", city.CountryId, DbType.Int32);

            sqlCommand = "Insert into Cities (CityName, CityPopulation, CountryId) " +
                "values(@CityName, @CityPopulation, @CountryId)";
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
            parameters.Add("@CityId", city.CityId, DbType.Int32);
            parameters.Add("@CityName", city.CityName, DbType.String);
            parameters.Add("@CityPopulation", city.CityPopulation, DbType.Int32);
            parameters.Add("@CountryId", city.CountryId, DbType.Int32);

            sqlCommand =
                "Update Cities " +
                "set CityName = @CityName, " +
                "CityPopulation = @CityPopulation, " +
                "CountryId = @CountryId " +
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
            parameters.Add("@CityId", CityId, DbType.Int32);

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