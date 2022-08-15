using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.RecipeModels
{
    public class RecipeElementViewModel
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
    }
}
