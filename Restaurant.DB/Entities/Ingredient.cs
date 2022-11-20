using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Restaurant.DB.Entities
{
    public class Ingredient
    {
        public Ingredient()
        {
            Meals = new List<Meal>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Meal> Meals { get; set; }
    }
}

