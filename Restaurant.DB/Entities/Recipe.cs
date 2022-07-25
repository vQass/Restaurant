using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.DB.Entities
{
    public class Recipe
    {
        [Required]
        public int IngredientId { get; set; }

        [Required]
        public int MealId { get; set; }

        [ForeignKey("MealId")]
        public virtual Meal Meal { get; set; }

        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }
    }
}
