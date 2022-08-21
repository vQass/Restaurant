using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.RecipeModels
{
    public class Recipe
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
