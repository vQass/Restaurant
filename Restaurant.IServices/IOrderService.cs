using Restaurant.Data.Models.OrderModels;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;

namespace Restaurant.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders(IEnumerable<OrderStatusEnum> orderStatuses, long userId = 0);
        Task<IEnumerable<OrderHistoryViewModel>> GetOrdersHistory(long userId = 0);
        Task<OrderAdminPanelWrapper> GetOrdersForAdminPanel(int pageIndex, int pageSize, string orderByParams);
        Order GetOrder(long id);
        long AddOrder(OrderCreateRequest orderCreateRequest);
        void UpdateOrder(long id, OrderUpdateRequest orderUpdateRequest);
        void ChangeOrderStatus(long id, OrderStatusEnum orderStatus);
    }
}
