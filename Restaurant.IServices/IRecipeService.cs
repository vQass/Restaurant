using Restaurant.Data.Models.RecipeModels;
using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.IServices
{
    public interface IRecipeService
    {
        public Task<IEnumerable<Recipe>> GetRecipes();
        public Recipe GetRecipe(int id);
        public RecipeElementViewModel GetRecipeElementViewModel(int mealId, int ingredientId);
        public string AddRecipeElement(RecipeCreateRequest recipeCreateRequest);
        public void DeleteRecipeElement(int mealId, int ingredientId);
    }
}
