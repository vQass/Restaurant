using Restaurant.Data.Models.OrderModels;
using Restaurant.Entities.Entities;
using Restaurant.Entities.Enums;

namespace Restaurant.Business.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders(IEnumerable<OrderStatusEnum> orderStatuses, long userId = 0, string orderByParams = null);
        Task<OrderHistoryWrapper> GetOrdersHistory(int pageIndex, int pageSize, long userId = 0, string orderByParams = null);
        Task<OrderAdminPanelWrapper> GetOrdersForAdminPanel(int pageIndex, int pageSize, string orderByParams);
        IEnumerable<OrderStatusViewModel> GetOrderStatuses();
        Order GetOrder(long id);
        long AddOrder(OrderCreateRequest orderCreateRequest);
        void UpdateOrder(long id, OrderUpdateRequest orderUpdateRequest);
        void ChangeOrderStatus(long id, OrderStatusEnum orderStatus);
    }
}
