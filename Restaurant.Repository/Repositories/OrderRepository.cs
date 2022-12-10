using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.IRepositories;
using Restaurant.Data.Exceptions;
using Restaurant.Data.Models.OrderModels;
using Restaurant.DB;
using Restaurant.Entities.Entities;
using Restaurant.Entities.Enums;
using Restaurant.LinqHelpers.Helpers;

namespace Restaurant.Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMealRepository _mealRepository;

        public OrderRepository(
            RestaurantDbContext dbContext,
            IMapper mapper,
            IMealRepository mealRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mealRepository = mealRepository;
        }

        #region GetMethods

        public Order GetOrder(long id)
        {
            return _dbContext.Orders
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Order> GetOrders(IEnumerable<OrderStatusEnum> orderStatuses = null, long userId = 0, int pageIndex = 0, int pageSize = 0, string orderByParams = null)
        {
            var ordersQuery = _dbContext.Orders
                .Include(x => x.OrderElements)
                .AsNoTracking();

            if (orderStatuses is not null)
            {
                ordersQuery = ordersQuery.Where(x => orderStatuses.Contains(x.Status));
            }

            if (userId != 0)
            {
                ordersQuery = ordersQuery.Where(x => x.UserId == userId);
            }

            ordersQuery = ordersQuery
                .ApplySorting(orderByParams)
                .ApplyPaging(pageIndex, pageSize);

            return ordersQuery.ToList();
        }

        public int GetOrdersCount(long userId = 0)
        {
            var orders = _dbContext.Orders.AsQueryable();

            if (userId != 0)
            {
                orders = orders.Where(x => x.UserId == userId);
            }

            return orders.Count();
        }

        #endregion

        #region EntityModificationMethods

        public long AddOrder(OrderCreateRequest orderCreateRequest)
        {
            var order = _mapper.Map<Order>(orderCreateRequest);

            order.OrderDate = DateTime.Now;
            order.Status = OrderStatusEnum.Pending;

            FillOrderMealsPrices(order);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return order.Id;
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

        #region Private Methods

        private void FillOrderMealsPrices(Order order)
        {
            var meals = _mealRepository.GetMeals().Result;

            foreach (var orderElement in order.OrderElements)
            {
                orderElement.CurrentPrice = meals.First(x => x.Id == orderElement.MealId).Price;
            }
        }

        #endregion
    }
}
