using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.RecipeModels
{
    public class RecipeCreateRequest
    {
        public int MealId { get; set; }
        public int IngredientId { get; set; }
    }
}
