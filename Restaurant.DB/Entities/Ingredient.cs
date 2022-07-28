using System.ComponentModel.DataAnnotations;

namespace Restaurant.DB.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Recipe> Recipes { get; set; }
    }
}

