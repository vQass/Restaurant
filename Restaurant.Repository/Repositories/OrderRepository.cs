using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.DB.Entities;
using Restaurant.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.DB.Enums;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models.OrderModels;
using AutoMapper;
using Restaurant.IRepository;

namespace Restaurant.Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderRepository(
            RestaurantDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region GetMethods

        public Order GetOrder(long id)
        {
            return _dbContext.Orders
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<Order>> GetOrders(IEnumerable<OrderStatusEnum> orderStatuses = null, long userId = 0)
        {
            var ordersQuery = _dbContext.Orders
                .Include(x => x.OrderElements)
                .AsNoTracking();

            if (orderStatuses is not null)
            {
                ordersQuery.Where(x => orderStatuses.Contains(x.Status));
            }

            if (userId != 0)
            {
                ordersQuery.Where(x => x.UserId == userId);
            }

            ordersQuery
                .OrderByDescending(x => x.Status)
                .ThenBy(x => x.OrderDate);

            return await ordersQuery.ToListAsync();
        }

        #endregion

        #region EntityModificationMethods

        public long AddOrder(OrderCreateRequest orderCreateRequest)
        {
            var order = _mapper.Map<Order>(orderCreateRequest);

            order.OrderDate = DateTime.Now;
            order.Status = OrderStatusEnum.Pending;
            // TODO get current price of a meal

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return order.Id;
        }

        public void UpdateOrder(Order order, OrderUpdateRequest orderUpdateRequest)
        {
            order.Address = orderUpdateRequest.Address;
            order.PhoneNumber = orderUpdateRequest.PhoneNumber;
            order.Surname = orderUpdateRequest.Surname;
            order.Name = orderUpdateRequest.Name;
            order.CityId = orderUpdateRequest.CityId;
            order.PromotionCodeId = orderUpdateRequest.PromotionId;

            _dbContext.SaveChanges();
        }

        public void ChangeOrderStatus(Order order, OrderStatusEnum orderStatus)
        {
            order.Status = orderStatus;
            _dbContext.SaveChanges();
        }

        #endregion

        #region ValidationMethods

        public void EnsureOrderExists(Order order)
        {
            if (order is null)
            {
                throw new NotFoundException("Podane zamówienie nie istnieje.");
            }
        }

        #endregion
    }
}
