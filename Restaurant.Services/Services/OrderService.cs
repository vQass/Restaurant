using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.OrderModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;
using Restaurant.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Services
{
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctors

        public OrderService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #endregion

        #region PublicMethods

        public Order GetOrderById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrders(List<OrderStatusEnum> orderStatuses)
        {
            var ordersQuery = GetFilteredOrdersQuery(orderStatuses);

            return await ordersQuery.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(long userId, List<OrderStatusEnum> orderStatuses)
        {
            var ordersQuery = GetFilteredOrdersQuery(orderStatuses, userId);

            return await ordersQuery.ToListAsync();
        }

        public long AddOrder(OrderCreateRequest orderCreateRequest)
        {
            var order = _mapper.Map<Order>(orderCreateRequest);

            order.OrderDate = DateTime.Now;
            order.Status = OrderStatusEnum.Pending;

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return order.Id;
        }

        public long UpdateOrder(long id, OrderUpdateRequest orderUpdateRequest)
        {
            var order = CheckIfOrderExists(id);

            order.Address = orderUpdateRequest.Address;
            order.PhoneNumber = orderUpdateRequest.PhoneNumber;
            order.Surname = orderUpdateRequest.Surname;
            order.Name = orderUpdateRequest.Name;
            order.CityId = orderUpdateRequest.CityId;
            order.PromotionCodeId = GetActivePromotionId(orderUpdateRequest.PromotionCode);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return order.Id;
        }

        public void ChangeOrderStatus(long id, OrderStatusEnum orderStatus)
        {
            var order = CheckIfOrderExists(id);
            order.Status = orderStatus;
            _dbContext.SaveChanges();
        }

        #endregion

        #region PrivateMethods

        private Order CheckIfOrderExists(long id)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.Id == id);

            if(order is null)
            {
                throw new NotFoundException("Zamówienie o podanym id nie istnieje");
            }

            return order;
        }

        private IQueryable<Order> GetFilteredOrdersQuery(List<OrderStatusEnum> orderStatuses, long userId = 0)
        {
            var ordersQuery = _dbContext.Orders.Include(x => x.OrderElements);

            if (orderStatuses is not null)
            {
                ordersQuery.Where(x => orderStatuses.Any(y => y == x.Status));
            }

            if(userId != 0)
            {
                ordersQuery.Where(x => x.UserId == userId);
            }

            return ordersQuery;
        }

        #endregion

        #region PrivateMethods

        private long GetActivePromotionId(string promotionCode)
        {
            var currentDateTime = DateTime.Now;
             var id = _dbContext.Promotions
                .FirstOrDefault(x => x.Code == promotionCode.Trim()
                    && x.StartDate <= currentDateTime
                    && x.EndDate >= currentDateTime)
                ?.Id;

            if(!id.HasValue)
            {
                throw new BadRequestException("Nie znaleziono aktywnej promocji o podanym kodzie.");
            }

            return id.Value;
        }

        #endregion
    }
}
