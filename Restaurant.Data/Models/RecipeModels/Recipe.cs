namespace Restaurant.Data.Models.RecipeModels
{
    public class Recipe
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
