using System.Net.Http.Json;

namespace BlazorCountriesWasm.Client.Services.CountryService
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _http;
        public CountryService(HttpClient http)
        {
            _http = http;
        }

        public List<Country> Countries { get; set; } = new List<Country>();

        public HttpClient? Http { get; }        //? here gets rid of green squiggly on "Public CountryService(HttpClient http)"

        public async Task CountryDelete(int? Countryid)
        {
            var result = await _http.DeleteAsync($"api/country/{Countryid}");
        }

        public async Task CountryInsert(Country country)
        {
            var result = await _http.PostAsJsonAsync("api/country", country); ;
        }

        public async Task CountryUpdate(int Countryid, Country country)
        {
            var result = await _http.PutAsJsonAsync($"api/country/{Countryid}", country);
        }

        public async Task GetCountries()
        {
            var result = await _http.GetFromJsonAsync<List<Country>>("api/country");
            if (result != null)
                Countries = result;
        }
        public async Task<Country> GetCountryById(int id)
        {
            var result = await _http.GetFromJsonAsync<Country>($"api/country/{id}");
            if (result != null)
                return result;
            throw new Exception("Country not found!");
        }

    }
}