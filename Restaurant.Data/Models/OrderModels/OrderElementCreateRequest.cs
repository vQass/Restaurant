namespace Restaurant.Data.Models.OrderModels
{
    public class OrderElementCreateRequest
    {
        public int MealId { get; set; }
        public short Amount { get; set; }
    }
}
