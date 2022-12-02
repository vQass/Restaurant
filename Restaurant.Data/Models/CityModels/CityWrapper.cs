using Restaurant.Entities.Entities;

namespace Restaurant.Data.Models.CityModels
{
    public class CityWrapper
    {
        public IEnumerable<City> Items { get; set; }
        public short ItemCount { get; set; }
    }
}
