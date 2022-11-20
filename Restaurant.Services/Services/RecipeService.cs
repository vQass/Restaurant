using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models.RecipeModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IRepository;
using Restaurant.IServices;

namespace Restaurant.Services.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        private readonly IMealRepository _mealRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly RestaurantDbContext _dbContext;

        public RecipeService(
            IRecipeRepository recipeRepository,
            IMapper mapper,
            IMealRepository mealRepository,
            IIngredientRepository ingredientRepository,
            RestaurantDbContext dbContext)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _mealRepository = mealRepository;
            _ingredientRepository = ingredientRepository;
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<Recipe>> GetRecipes()
        {
            return await _recipeRepository.GetRecipes();
        }

        public Recipe GetRecipe(int mealId)
        {
            var recipe = _recipeRepository.GetRecipe(mealId);

            _recipeRepository.EnsureRecipeExists(recipe);

            return recipe;
        }

        public RecipeElementViewModel GetRecipeElementViewModel(int mealId, int ingredientId)
        {
            var recipeElement = _recipeRepository.GetRecipeElement(mealId, ingredientId);

            _recipeRepository.EnsureRecipeElementExists(recipeElement);

            return _mapper.Map<RecipeElementViewModel>(recipeElement);
        }

        public RecipeEditViewModel GetRecipeEditViewModel(int mealId)
        {
            var recipe = _recipeRepository.GetRecipe(mealId);

            var ids = recipe.RecipeIngredients
                .Select(x => x.IngredientId)
                .ToList();

            var ingredientsNotInRecipe = _dbContext.Ingredients
                .Where(x => !ids.Contains(x.Id))
                .ToList();

            var recipeEditVM = new RecipeEditViewModel
            {
                MealId = recipe.MealId,
                MealName = recipe.MealName,
                IngredientsIncludedInRecipe = recipe.RecipeIngredients,
                IngredientsNotIncludedInRecipe = ingredientsNotInRecipe.Select(x => new RecipeIngredient
                {
                    IngredientId = x.Id,
                    IngredientName = x.Name
                }).ToList()
            };

            return recipeEditVM;
        }

        public string AddRecipeElement(RecipeCreateRequest recipeCreateRequest)
        {
            _recipeRepository.EnsureRecipeElementDoesNotExists(recipeCreateRequest);

            var combinedIds = _recipeRepository.AddRecipeElement(new RecipeElement()
            {
                MealId = recipeCreateRequest.MealId,
                IngredientId = recipeCreateRequest.IngredientId
            });

            return combinedIds;
        }

        public void DeleteRecipeElement(int mealId, int ingredientId)
        {
            var recipeElement = _recipeRepository.GetRecipeElement(mealId, ingredientId);

            _recipeRepository.EnsureRecipeElementExists(recipeElement);

            _recipeRepository.DeleteRecipeElement(recipeElement);
        }

        public async Task UpdateMealRecipe(int mealId, List<int> ingredientsIds)
        {
            var meal = _dbContext.Meals.Include(x => x.RecipeElements).FirstOrDefault(x => x.Id == mealId);
            
            _mealRepository.EnsureMealExists(meal);

            var toAdd = meal.RecipeElements
                .Where(x => !ingredientsIds.Contains(x.IngredientId))
                .Select(x => x.IngredientId)
                .ToList();

            var toRemove = ingredientsIds
                .Where(x => !meal.RecipeElements
                    .Select(x => x.IngredientId)
                    .Contains(x))
                .ToList();

            var ingrToAdd = await _ingredientRepository.GetIngredients(toAdd);

            var ingrToRemove = await _ingredientRepository.GetIngredients(toAdd);

            //meal.RecipeElements.AddRange();

            // TODO add to db save changes and check if it works
        }
    }
}
