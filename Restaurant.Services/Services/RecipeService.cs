using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.RecipeModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Services
{
    public class RecipeService : IRecipeService
    {
        #region Fields

        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctors

        public RecipeService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #endregion

        #region PublicMethods

        public async Task<IEnumerable<RecipeViewModel>> GetRecipies()
        {
            var recipes = await _dbContext.Meals
                .Include(x => x.RecipeElements)
                .ThenInclude(x => x.Ingredient)
                .AsNoTracking()
                .ToListAsync();

            var recipesViewModels = _mapper.Map<List<RecipeViewModel>>(recipes);

            return recipesViewModels;
        }

        public RecipeViewModel GetRecipeByMealId(int mealId)
        {
            var recipe = CheckIfRecipeExistsByMealId(mealId);

            var recipeViewModel = _mapper.Map<RecipeViewModel>(recipe);

            return recipeViewModel;
        }

        public RecipeElementViewModel GetRecipeElementViewModel(int mealId, int ingredientId)
        {
            var recipeElement = GetRecipeElement(mealId, ingredientId);

            var recipeElementViewModel = _mapper.Map<RecipeElementViewModel>(recipeElement);

            return recipeElementViewModel;
        }

        public string AddRecipeElement(RecipeCreateRequest recipeCreateRequest)
        {
            CheckIfRecipeElementAlreadyExists(recipeCreateRequest.MealId, recipeCreateRequest.IngredientId);

            var recipeElement = new RecipeElement();
            recipeElement.MealId = recipeCreateRequest.MealId;
            recipeElement.IngredientId = recipeCreateRequest.IngredientId;

            _dbContext.Recipes.Add(recipeElement);
            _dbContext.SaveChanges();

            return recipeElement.MealId.ToString() + "/" + recipeElement.IngredientId.ToString();
        }

        public void DeleteRecipeElement(int mealId, int ingredientId)
        {
            var recipeElement = CheckIfRecipeElementExists(mealId, ingredientId);

            _dbContext.Recipes.Remove(recipeElement);
            _dbContext.SaveChanges();
        }

        #endregion

        #region PrivateMethods

        private Meal CheckIfRecipeExistsByMealId(int mealId)
        {
            var recipeElement = _dbContext.Meals
                .Include(x => x.RecipeElements)
                .ThenInclude(x => x.Ingredient)
                .FirstOrDefault(x => x.Id == mealId);

            if (recipeElement is null)
            {
                throw new NotFoundException("Przepis posiłku o podanym id nie istnieje.");
            }

            return recipeElement;
        }

        private RecipeElement GetRecipeElement(int mealId, int ingredientId)
        {
            var recipeElement = _dbContext.Recipes
                .Include(x => x.Ingredient)
                .Include(x => x.Meal)
                .FirstOrDefault(
                    x => x.MealId == mealId
                    && x.IngredientId == ingredientId);

            return recipeElement;
        }

        private RecipeElement CheckIfRecipeElementExists(int mealId, int ingredientId)
        {
            var recipeElement = GetRecipeElement(mealId, ingredientId);

            if (recipeElement is null)
            {
                throw new NotFoundException("Element przepisu nie istnieje.");
            }

            return recipeElement;
        }

        private void CheckIfRecipeElementAlreadyExists(int mealId, int ingredientId)
        {
            var recipeElement = GetRecipeElement(mealId, ingredientId);

            if (recipeElement != null)
            {
                throw new BadRequestException("Element przepisu już istnieje.");
            }
        }

        #endregion
    }
}
