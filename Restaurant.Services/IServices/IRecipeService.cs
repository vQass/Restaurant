using Restaurant.Data.Models.RecipeModels;

namespace Restaurant.Business.IServices
{
    public interface IRecipeService
    {
        Recipe GetRecipe(int mealId);
        RecipeEditViewModel GetRecipeEditViewModel(int mealId);
        Task UpdateMealRecipe(int mealId, List<int> ingredientsIds);
    }
}
