namespace Restaurant.Data.Models.RecipeModels
{
    public class RecipeEditViewModel
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public List<RecipeEditIngredient> Ingredients { get; set; }
    }
}
