using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models.RecipeModels;
using Restaurant.DB;
using Restaurant.IRepository;
using Restaurant.IServices;
using System.Runtime.CompilerServices;

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


        public Recipe GetRecipe(int mealId)
        {
            var recipe = _recipeRepository.GetRecipe(mealId);

            return recipe;
        }

        public RecipeEditViewModel GetRecipeEditViewModel(int mealId)
        {
            //var recipe = _recipeRepository.GetRecipe(mealId);

            //var ids = recipe.RecipeIngredients
            //    .Select(x => x.IngredientId)
            //    .ToList();

            //var ingredientsNotInRecipe = _dbContext.Ingredients
            //    .Where(x => !ids.Contains(x.Id))
            //    .ToList();

            //var recipeEditVM = new RecipeEditViewModel
            //{
            //    MealId = recipe.MealId,
            //    MealName = recipe.MealName,
            //    IngredientsIncludedInRecipe = recipe.RecipeIngredients,
            //    IngredientsNotIncludedInRecipe = ingredientsNotInRecipe.Select(x => new RecipeIngredient
            //    {
            //        IngredientId = x.Id,
            //        IngredientName = x.Name
            //    }).ToList()
            //};

            //return recipeEditVM;

            var meal = _mealRepository.GetMeal(mealId);

            _mealRepository.EnsureMealExists(meal);

            var recipe = _dbContext.Ingredients.Include(x => x.Meals).Select(x => new RecipeEditIngredient()
            {
                IngredientId = x.Id,
                IngredientName = x.Name,
                IsInRecipe = x.Meals.Any(x => x.Id == mealId)
            }).ToList();

            return new RecipeEditViewModel()
            {
                MealId = mealId,
                MealName = meal.Name,
                Ingredients = recipe
            };
        }

        public async Task UpdateMealRecipe(int mealId, List<int> ingredientsIds)
            {
                var meal = _dbContext.Meals.Include(x => x.Ingredients).FirstOrDefault(x => x.Id == mealId);

                _mealRepository.EnsureMealExists(meal);

                var ingredientsInRecipeIds = meal.Ingredients.Select(x => x.Id).ToList();

                var toAddIds = ingredientsIds.Where(x => !ingredientsInRecipeIds.Contains(x)).ToList();

                var ingredientsToAdd = _ingredientRepository.GetIngredients(toAddIds);

                var toRemoveIds = ingredientsInRecipeIds
                    .Except(ingredientsIds)
                    .ToList();

                var toRemove = meal.Ingredients
                    .Where(x => toRemoveIds.Contains(x.Id))
                    .ToList();

                meal.Ingredients.AddRange(ingredientsToAdd);

                foreach (var item in toRemove)
                {
                    meal.Ingredients.Remove(item);
                }

                _dbContext.SaveChanges();
            }
        }
    }
