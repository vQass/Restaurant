using Restaurant.Data.Models.RecipeModels;

namespace Restaurant.Business.IServices
{
    public interface IRecipeService
    {
        RecipeEditViewModel GetRecipeEditViewModel(int mealId);
        void UpdateMealRecipe(int mealId, List<int> ingredientsIds);
    }
}
