namespace BlazorCountriesWasm.Client.Services.CountryService
{
    public interface ICountryService
    {
        List<Country> Countries { get; set; }

        Task GetCountries();
        Task<Country> GetCountryById(int id);
    }
}