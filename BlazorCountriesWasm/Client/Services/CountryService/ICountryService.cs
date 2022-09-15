namespace BlazorCountriesWasm.Client.Services.CountryService
{
    public interface ICountryService
    {
        List<Country> Countries { get; set; }

        Task GetCountries();
        Task<Country> GetCountryById(int Countryid);
        Task CountryInsert(Country country);
        Task CountryUpdate(int Countryid, Country country);
        Task CountryDelete(int? Countryid);
    }
}