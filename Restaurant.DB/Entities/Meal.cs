namespace Restaurant.DB.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; } = false;
        public short MealCategoryId { get; set; }
        public virtual MealCategory MealCategory { get; set; }
        public virtual List<OrderElement> OrderElements { get; set; }
        public virtual List<RecipeElement> RecipeElements { get; set; }
    }
}
