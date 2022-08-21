using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.RecipeModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IRepository;
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
        private readonly IRecipeRepository _recipeRepository;
        #region Fields

        private readonly IMapper _mapper;

        #endregion

        #region Ctors

        public RecipeService(
            IRecipeRepository recipeRepository,
            IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        #endregion

        #region PublicMethods

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

        #endregion
    }
}
