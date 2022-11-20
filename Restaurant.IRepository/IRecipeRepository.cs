using Restaurant.Data.Models.RecipeModels;

namespace Restaurant.IRepository
{
    public interface IRecipeRepository
    {
        Recipe GetRecipe(int mealId);
    }
}