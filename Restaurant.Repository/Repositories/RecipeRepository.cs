using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Business.IRepositories;
using Restaurant.Data.Models.RecipeModels;
using Restaurant.DB;
using Restaurant.Entities.Entities;

namespace Restaurant.Repository.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public RecipeRepository(
            RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
 
        }

        public List<RecipeEditIngredient> GetIngredientsForMeal(int mealId)
        {
            var recipe = _dbContext.Ingredients.Include(x => x.Meals).Select(x => new RecipeEditIngredient()
            {
                IngredientId = x.Id,
                IngredientName = x.Name,
                IsInRecipe = x.Meals.Any(x => x.Id == mealId)
            }).ToList();

            return recipe;
        }

        public Meal GetMealWithRecipe(int mealId)
        {
            return _dbContext.Meals.Include(x => x.Ingredients).FirstOrDefault(x => x.Id == mealId);
        }

        public void UpdateRecipe(Meal meal, List<Ingredient> toAdd, List<Ingredient> toRemove)
        {
            meal.Ingredients.AddRange(toAdd);

            foreach (var item in toRemove)
            {
                meal.Ingredients.Remove(item);
            }

            _dbContext.SaveChanges();
        }
    }
}
