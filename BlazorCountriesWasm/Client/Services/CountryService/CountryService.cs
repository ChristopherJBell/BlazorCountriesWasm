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

        public async Task GetCountries()
        {
            var result = await _http.GetFromJsonAsync<List<Country>>("api/country");
            if (result != null)
                Countries = result;
        }
        public Task<Country> GetCountryById(int id)
        {
            throw new NotImplementedException();
        }
    }
}