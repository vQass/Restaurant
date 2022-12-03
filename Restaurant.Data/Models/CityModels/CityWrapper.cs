using Restaurant.Entities.Entities;

namespace Restaurant.Data.Models.CityModels
{
    public class CityWrapper
    {
        public IEnumerable<CityViewModel> Items { get; set; }
        public short ItemCount { get; set; }
    }
}
