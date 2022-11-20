namespace Restaurant.Data.Models.RecipeModels
{
    public class RecipeEditViewModel
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public List<RecipeIngredient> IngredientsIncludedInRecipe { get; set; }
        public List<RecipeIngredient> IngredientsNotIncludedInRecipe { get; set; }
    }
}
