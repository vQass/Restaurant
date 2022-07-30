using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.DB.Entities
{
    public class RecipeElement
    {
        public int Id { get; set; }
        public virtual Meal Meal { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
