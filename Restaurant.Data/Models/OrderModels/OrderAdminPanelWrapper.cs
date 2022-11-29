namespace Restaurant.Data.Models.OrderModels
{
    public class OrderAdminPanelWrapper
    {
        public List<OrderAdminPanelViewModel> Items { get; set; }
        public int ItemsCount { get; set; }
    }
}
