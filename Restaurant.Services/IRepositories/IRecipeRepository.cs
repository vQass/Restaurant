using Restaurant.Data.Models.RecipeModels;

namespace Restaurant.Business.IRepositories
{
    public interface IRecipeRepository
    {
        Recipe GetRecipe(int mealId);
    }
}