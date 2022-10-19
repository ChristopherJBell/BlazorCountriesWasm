using System.ComponentModel.DataAnnotations;

namespace BlazorCountriesWasm.Shared
{
    public class City
    {
        public int CityId { get; set; }
        [Required(ErrorMessage = "A City Name is required.")]
        [StringLength(50, ErrorMessage = "Name is too long - it cannot be longer than 50 characters.")]
        public string CityName { get; set; } = string.Empty;
        [Required]
        [Range(0, 25000000, ErrorMessage = "Population must be less than 25 million")]
        public int CityPopulation { get; set; } = 0;
        public int CountryId { get; set; }
    }
}