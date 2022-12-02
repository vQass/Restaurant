namespace Restaurant.Data.Models.MealModels
{
    public class MealUpdateRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte MealCategoryId { get; set; }
    }
}
