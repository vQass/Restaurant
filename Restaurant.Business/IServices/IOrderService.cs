using Restaurant.Data.Models.OrderModels;
using Restaurant.Entities.Enums;

namespace Restaurant.Business.IServices
{
    public interface IOrderService
    {
        Task<OrderHistoryWrapper> GetOrdersHistory(int pageIndex, int pageSize, long userId = 0, string orderByParams = null);
        Task<OrderAdminPanelWrapper> GetOrdersForAdminPanel(int pageIndex, int pageSize, string orderByParams);
        IEnumerable<OrderStatusViewModel> GetOrderStatuses();

        long AddOrder(OrderCreateRequest orderCreateRequest);
        void ChangeOrderStatus(long id, OrderStatusEnum orderStatus);
    }
}
