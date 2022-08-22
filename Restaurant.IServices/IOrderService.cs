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
        public Task<IEnumerable<Order>> GetOrders(IEnumerable<OrderStatusEnum> orderStatuses);
        public Order GetOrder(long id);
        public long AddOrder(OrderCreateRequest orderCreateRequest);
        void UpdateOrder(long id, OrderUpdateRequest orderUpdateRequest);
        public void ChangeOrderStatus(long id, OrderStatusEnum orderStatus);
    }
}
