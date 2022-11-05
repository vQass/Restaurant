
namespace Restaurant.Data.Models.MealModels
{
    public class MealCreateRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte CategoryId { get; set; }
    }
}
