namespace Restaurant.DB.Entities
{
    public class MealCategory
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public virtual List<Meal> Meals { get; set; }
    }
}
