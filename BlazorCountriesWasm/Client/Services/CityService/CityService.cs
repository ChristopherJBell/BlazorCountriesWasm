using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace BlazorCountriesWasm.Client.Services.CityService
{
    public class CityService : ICityService
    {
        private readonly HttpClient _http;
        public CityService(HttpClient http)
        {
            _http = http;
        }

        public List<City> Cities { get; set; } = new List<City>();

        public HttpClient? Http { get; }        //? here gets rid of green squiggly on "Public CityService(HttpClient http)"

        public async Task CityDelete(int Cityid)
        {
            var result = await _http.DeleteAsync($"api/city/{Cityid}");
        }

        public async Task CityInsert(City city)
        {
            var result = await _http.PostAsJsonAsync("api/city/", city); ;
        }

        public async Task CityUpdate(int Cityid, City city)
        {
            var result = await _http.PutAsJsonAsync($"api/city/{Cityid}", city);
        }

        public async Task GetCities()
        {
            var result = await _http.GetFromJsonAsync<List<City>>("api/city/");
            Cities = result;
        }

        public async Task GetCitiesByCountryId(int CountryId)
        {
            var result = await _http.GetFromJsonAsync<List<City>>($"api/citiesbycountryid/{CountryId}");
            Cities = result;
        }

        public async Task<City> GetCityById(int id)
        {
            var result = await _http.GetFromJsonAsync<City>($"api/city/{id}");
            if (result != null)
                return result;
            throw new Exception("City not found!");
        }

        public async Task<int> CountCitiesForInsert(int CountryId, string cityName)
        {
            var result = await _http.GetFromJsonAsync<int>($"api/city/{CountryId}/{cityName}");
            return result;
        }

        public async Task<int> CountCitiesForEdit(int CountryId, string cityName, int CityId)
        {
            var result = await _http.GetFromJsonAsync<int>($"api/city/{CountryId}/{cityName}/{CityId}");
            return result;
        }

    }
}