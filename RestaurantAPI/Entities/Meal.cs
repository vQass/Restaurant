namespace RestaurantAPI.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
    }
}
