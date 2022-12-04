using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.IRepositories;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.RecipeModels;
using Restaurant.DB;

namespace Restaurant.Business.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMealRepository _mealRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public RecipeService(
            IRecipeRepository recipeRepository,
            IMealRepository mealRepository,
            IIngredientRepository ingredientRepository)
        {
            _recipeRepository = recipeRepository;
            _mealRepository = mealRepository;
            _ingredientRepository = ingredientRepository;
        }

        public RecipeEditViewModel GetRecipeEditViewModel(int mealId)
        {
            var meal = _mealRepository.GetMeal(mealId);

            _mealRepository.EnsureMealExists(meal);

            var recipe = _recipeRepository.GetIngredientsForMeal(mealId);

            return new RecipeEditViewModel()
            {
                MealId = mealId,
                MealName = meal.Name,
                Ingredients = recipe
            };
        }

        public void UpdateMealRecipe(int mealId, List<int> ingredientsIds)
        {
            var meal = _recipeRepository.GetMealWithRecipe(mealId);

            _mealRepository.EnsureMealExists(meal);

            var ingredientsInRecipeIds = meal.Ingredients
                .Select(x => x.Id)
                .ToList();

            var toAddIds = ingredientsIds
                .Where(x => !ingredientsInRecipeIds.Contains(x))
                .ToList();

            var ingredientsToAdd = _ingredientRepository
                .GetIngredients(toAddIds)
                .ToList();

            var toRemoveIds = ingredientsInRecipeIds
                .Except(ingredientsIds)
                .ToList();

            var ingredientsToRemove = meal.Ingredients
                .Where(x => toRemoveIds.Contains(x.Id))
                .ToList();

            _recipeRepository.UpdateRecipe(meal, ingredientsToAdd, ingredientsToRemove);
        }
    }
}
