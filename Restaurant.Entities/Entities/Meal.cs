namespace Restaurant.Entities.Entities
{
    public class Meal
    {
        public Meal()
        {
            Ingredients = new List<Ingredient>();
            OrderElements = new List<OrderElement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; } = false;
        public short MealCategoryId { get; set; }

        public virtual MealCategory MealCategory { get; set; }
        public virtual List<OrderElement> OrderElements { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
    }
}
