using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCountriesWasm.Shared
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public int CityPopulation { get; set; } = 0;
        public int CountryId { get; set; }
    }
}


