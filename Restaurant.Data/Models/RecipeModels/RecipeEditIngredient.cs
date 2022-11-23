namespace Restaurant.Data.Models.RecipeModels
{
    public class RecipeEditIngredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public bool IsInRecipe { get; set; }
    }
}
