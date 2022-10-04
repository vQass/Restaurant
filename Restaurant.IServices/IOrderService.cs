using Restaurant.Data.Models.OrderModels;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;

namespace Restaurant.IServices
{
    public interface IOrderService
    {
        public Task<IEnumerable<Order>> GetOrders(IEnumerable<OrderStatusEnum> orderStatuses, long userId = 0);
        public Task<IEnumerable<OrderHistoryViewModel>> GetOrdersHistory(long userId = 0);
        public Order GetOrder(long id);
        public long AddOrder(OrderCreateRequest orderCreateRequest);
        void UpdateOrder(long id, OrderUpdateRequest orderUpdateRequest);
        public void ChangeOrderStatus(long id, OrderStatusEnum orderStatus);
    }
}
