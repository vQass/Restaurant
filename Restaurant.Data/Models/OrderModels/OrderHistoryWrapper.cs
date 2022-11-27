namespace Restaurant.Data.Models.OrderModels
{
    public class OrderHistoryWrapper
    {
        public List<OrderHistoryViewModel> Items { get; set; }
        public int ItemCount { get; set; }
    }
}
