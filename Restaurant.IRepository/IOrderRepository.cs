using Restaurant.Data.Models.OrderModels;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;

namespace Restaurant.IRepository
{
    public interface IOrderRepository
    {
        long AddOrder(OrderCreateRequest orderCreateRequest);
        void ChangeOrderStatus(Order order, OrderStatusEnum orderStatus);
        void EnsureOrderExists(Order order);
        Order GetOrder(long id);
        Task<List<Order>> GetOrders(IEnumerable<OrderStatusEnum> orderStatuses = null, long userId = 0);
        void UpdateOrder(Order order, OrderUpdateRequest orderUpdateRequest);
    }
}