using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.RecipeModels
{
    public class RecipeViewModel
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public List<RecipeIngredientViewModel> RecipeElementViewModels { get; set; }
    }
}
