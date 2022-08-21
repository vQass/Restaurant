using Restaurant.Data.Models.RecipeModels;
using Restaurant.DB.Entities;

namespace Restaurant.IRepository
{
    public interface IRecipeRepository
    {
        string AddRecipeElement(RecipeElement recipeElement);
        void DeleteRecipeElement(RecipeElement recipeElement);
        void EnsureRecipeElementDoesNotExists(RecipeCreateRequest recipeCreateRequest);
        void EnsureRecipeElementExists(RecipeElement recipeElement);
        void EnsureRecipeExists(Recipe recipe);
        Recipe GetRecipe(int mealId);
        RecipeElement GetRecipeElement(int mealId, int ingredientId);
        Task<List<Recipe>> GetRecipes();
    }
}