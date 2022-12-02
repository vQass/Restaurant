using Restaurant.Data.Models.IngredientModels;

namespace Restaurant.Data.Models.MealModels
{
    public class MealViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }
    }
}
