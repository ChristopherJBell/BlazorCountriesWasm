namespace BlazorCountriesWasm.Client.Services.CityService
{
    public interface ICityService
    {
        List<City> Cities { get; set; }
        Task GetCities();
        Task<City> GetCityById(int Cityid);
        Task GetCitiesByCountryId(int CountryId);
        Task CityInsert(City city);
        Task CityUpdate(int Cityid, City city);
        Task CityDelete(int Cityid);
        Task<int> CountCitiesByName(string cityName);
        Task<int> CountCitiesByNameAndId(string cityName, int id);
    }
}