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
        Task<int> CountCitiesForInsert(int CountryId, string cityName);
        Task<int> CountCitiesForEdit(int CountryId, string cityName, int CityId);
    }
}