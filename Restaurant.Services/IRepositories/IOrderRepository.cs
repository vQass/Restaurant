using Restaurant.Data.Models.OrderModels;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;

namespace Restaurant.Business.IRepositories
{
    public interface IOrderRepository
    {
        Order GetOrder(long id);
        Task<List<Order>> GetOrders(IEnumerable<OrderStatusEnum> orderStatuses = null, long userId = 0, int pageIndex = 0, int pageSize = 0, string orderByParams = null);
        int GetOrdersCount(long userId = 0);

        long AddOrder(OrderCreateRequest orderCreateRequest);
        void UpdateOrder(Order order, OrderUpdateRequest orderUpdateRequest);
        void ChangeOrderStatus(Order order, OrderStatusEnum orderStatus);

        void EnsureOrderExists(Order order);
    }
}