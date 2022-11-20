using Restaurant.Data.Models.RecipeModels;

namespace Restaurant.IServices
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetRecipes();
        RecipeEditViewModel GetRecipeEditViewModel(int mealId);
        Recipe GetRecipe(int id);
        string AddRecipeElement(RecipeCreateRequest recipeCreateRequest);
        void DeleteRecipeElement(int mealId, int ingredientId);
        Task UpdateMealRecipe(int mealId, List<int> ingredientsIds);
    }
}
