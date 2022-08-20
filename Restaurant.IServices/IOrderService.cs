using Restaurant.Data.Models.OrderModels;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.IServices
{
    public interface IOrderService
    {
        public Task<IEnumerable<Order>> GetOrders(List<OrderStatusEnum> orderStatuses);
        public Task<IEnumerable<Order>> GetOrdersByUserId(long userId, List<OrderStatusEnum> orderStatuses);
        public Order GetOrderById(long id);
        public long AddOrder(OrderCreateRequest orderCreateRequest);
        public void ChangeOrderStatus(long id, OrderStatusEnum orderStatus);
    }
}
