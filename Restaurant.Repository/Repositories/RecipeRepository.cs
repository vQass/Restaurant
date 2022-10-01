using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.DB.Entities;
using Restaurant.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Data.Models.RecipeModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.IRepository;

namespace Restaurant.Repository.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<RecipeRepository> _logger;
        private readonly IMapper _mapper;

        public RecipeRepository(
            RestaurantDbContext dbContext,
            ILogger<RecipeRepository> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        #region GetMethods

        public async Task<List<Recipe>> GetRecipes()
        {
            var mealsWithRecipe = _dbContext.Meals
                .Include(x => x.RecipeElements)
                .ThenInclude(x => x.Ingredient)
                .ToList();

            return _mapper.Map<List<Recipe>>(mealsWithRecipe); ;
        }

        public Recipe GetRecipe(int mealId)
        {
            var mealWithRecipe = _dbContext.Meals
                .Include(x => x.RecipeElements)
                .ThenInclude(x => x.Ingredient)
                .FirstOrDefault(x => x.Id == mealId);

            var recipe = _mapper.Map<Recipe>(mealWithRecipe);

            return recipe;
        }

        public RecipeElement GetRecipeElement(int mealId, int ingredientId)
        {
            var recipeElement = _dbContext.Recipes
                .Include(x => x.Ingredient)
                .Include(x => x.Meal)
                .FirstOrDefault(
                    x => x.MealId == mealId
                    && x.IngredientId == ingredientId);

            return recipeElement;
        }

        #endregion

        #region EntityModificationMethods

        public string AddRecipeElement(RecipeElement recipeElement)
        {
            _dbContext.Recipes.Add(recipeElement);
            _dbContext.SaveChanges();

            var idsCombined = recipeElement.MealId.ToString() + "/" + recipeElement.IngredientId.ToString();

            return idsCombined;
        }

        public void DeleteRecipeElement(RecipeElement recipeElement)
        {
            _dbContext.Recipes.Remove(recipeElement);
            _dbContext.SaveChanges();
        }

        #endregion

        #region ValidationMethods

        public void EnsureRecipeExists(Recipe recipe)
        {
            if (recipe is null)
            {
                throw new NotFoundException("Podany przepis nie istnieje.");
            }
        }

        public void EnsureRecipeElementExists(RecipeElement recipeElement)
        {
            if (recipeElement is null)
            {
                throw new NotFoundException("Podany element przepisu nie istnieje.");
            }
        }

        public void EnsureRecipeElementDoesNotExists(RecipeCreateRequest recipeCreateRequest)
        {
            var recipeElementExists = _dbContext.Recipes
                .Include(x => x.Ingredient)
                .Include(x => x.Meal)
                .Any(
                    x => x.MealId == recipeCreateRequest.MealId
                    && x.IngredientId == recipeCreateRequest.IngredientId);

            if (recipeElementExists)
            {
                _logger.LogError($"Recipe element with mealId: {recipeCreateRequest.MealId}" +
                    $" and ingredientId: {recipeCreateRequest.IngredientId} already exists.");
                throw new NotFoundException("Element przepisu już istnieje.");
            }
        }

        #endregion
    }
}
