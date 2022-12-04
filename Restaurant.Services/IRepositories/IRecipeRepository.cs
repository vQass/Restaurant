using Restaurant.Data.Models.RecipeModels;
using Restaurant.Entities.Entities;

namespace Restaurant.Business.IRepositories
{
    public interface IRecipeRepository
    {
        List<RecipeEditIngredient> GetIngredientsForMeal(int mealId);

        Meal GetMealWithRecipe(int mealId);

        void UpdateRecipe(Meal meal, List<Ingredient> toAdd, List<Ingredient> toRemove);
    }
}