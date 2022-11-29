namespace Restaurant.Data.Models.OrderModels
{
    public class OrderElementUpdateRequest
    {
        public int MealId { get; set; }
        public short Amount { get; set; }
    }
}
