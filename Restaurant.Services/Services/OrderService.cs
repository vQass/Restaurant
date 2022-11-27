using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.OrderModels;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;
using Restaurant.IRepository;
using Restaurant.IServices;

namespace Restaurant.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMealRepository _mealRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IPromotionRepository promotionRepository,
            ICityRepository cityRepository,
            IMealRepository mealRepository,
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _promotionRepository = promotionRepository;
            _cityRepository = cityRepository;
            _mealRepository = mealRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<OrderStatusViewModel> GetOrderStatuses()
        {
            var statuses = OrderStatusDictionary.OrderStatusesWithDescription;

            var statusesVM = statuses.Select(x => new OrderStatusViewModel
            {
                Id = x.Key,
                Tag = OrderStatusDictionary.OrderStatusesTags.GetValueOrDefault((byte)x.Key),
                Description = x.Value
            });

            return statusesVM;
        }

        public Order GetOrder(long id)
        {
            var order = _orderRepository.GetOrder(id);

            _orderRepository.EnsureOrderExists(order);

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrders(IEnumerable<OrderStatusEnum> orderStatuses = null, long userId = 0, string orderByParams = null)
        {
            return await _orderRepository.GetOrders(orderStatuses, userId, orderByParams: orderByParams);
        }

        public async Task<OrderHistoryWrapper> GetOrdersHistory(int pageIndex, int pageSize, long userId = 0, string orderByParams = null)
        {
            var orders = await _orderRepository.GetOrders(
                pageIndex: pageIndex,
                pageSize: pageSize,
                userId: userId,
                orderByParams: orderByParams);

            var cities = _cityRepository.GetCities();

            var meals = await _mealRepository.GetMeals();

            var ordersHistory = orders.Select(x => new OrderHistoryViewModel()
            {
                Name = x.Name,
                Surname = x.Surname,
                Address = x.Address,
                City = cities.FirstOrDefault(y => y.Id == x.CityId).Name,
                OrderDate = x.OrderDate,
                PhoneNumber = x.PhoneNumber,
                Status = OrderStatusDictionary.OrderStatusesWithDescription.GetValueOrDefault((byte)x.Status),
                StatusTag = OrderStatusDictionary.OrderStatusesTags.GetValueOrDefault((byte)x.Status),
                OrderElements = x.OrderElements
                    .Select(y => new OrderElementViewModel()
                    {
                        Amount = y.Amount,
                        MealName = meals.FirstOrDefault(z => z.Id == y.MealId).Name,
                        Price = y.CurrentPrice
                    })
                    .ToList()
            }).ToList();

            _orderRepository.GetOrdersCount(userId);

            var orderWrapper = new OrderHistoryWrapper
            {
                Items = ordersHistory,
                ItemCount = _orderRepository.GetOrdersCount(userId),
            };

            return orderWrapper;
        }

        public async Task<OrderAdminPanelWrapper> GetOrdersForAdminPanel(int pageIndex, int pageSize, string orderByParams)
        {
            var orders = await _orderRepository.GetOrders(pageIndex: pageIndex, pageSize: pageSize, orderByParams: orderByParams);

            var userIds = orders.Select(x => x.UserId).ToList();

            var users = await _userRepository.GetUsers(userIds);

            var cities = _cityRepository.GetCities(null);

            var meals = await _mealRepository.GetMeals();

            var ordersVM = orders.Select(x => new OrderAdminPanelViewModel()
            {
                Id = x.Id,
                Email = users.FirstOrDefault(y => y.Id == x.UserId).Email,
                Name = x.Name,
                Surname = x.Surname,
                Address = x.Address,
                City = cities.FirstOrDefault(y => y.Id == x.CityId).Name,
                OrderDate = x.OrderDate,
                PhoneNumber = x.PhoneNumber,
                Status = OrderStatusDictionary.OrderStatusesWithDescription.GetValueOrDefault((byte)x.Status),
                OrderElements = x.OrderElements
                    .Select(y => new OrderElementViewModel()
                    {
                        Amount = y.Amount,
                        MealName = meals.FirstOrDefault(z => z.Id == y.MealId).Name,
                        Price = y.CurrentPrice
                    })
                    .ToList()
            }).ToList();

            var ordersWrapper = new OrderAdminPanelWrapper()
            {
                Items = ordersVM,
                ItemsCount = _orderRepository.GetOrdersCount()
            };

            return ordersWrapper;
        }

        public long AddOrder(OrderCreateRequest orderCreateRequest)
        {
            if (orderCreateRequest.PromotionCode != null)
            {
                var promotion = _promotionRepository.GetPromotion(orderCreateRequest.PromotionCode);

                _promotionRepository.EnsurePromotionExists(promotion);

                _promotionRepository.EnsurePromotionIsActive(promotion);

                orderCreateRequest.PromotionId = promotion?.Id;
            }

            var id = _orderRepository.AddOrder(orderCreateRequest);

            return id;
        }

        public void UpdateOrder(long id, OrderUpdateRequest orderUpdateRequest)
        {
            var order = _orderRepository.GetOrder(id);

            _orderRepository.EnsureOrderExists(order);

            EnsureOrderHasPendingStatus(order);

            var promotion = _promotionRepository.GetPromotion(orderUpdateRequest.PromotionCode);

            _promotionRepository.EnsurePromotionExists(promotion);

            _promotionRepository.EnsurePromotionIsActive(promotion);

            orderUpdateRequest.PromotionId = promotion?.Id;

            _orderRepository.UpdateOrder(order, orderUpdateRequest);
        }

        public void ChangeOrderStatus(long id, OrderStatusEnum orderStatus)
        {
            var order = _orderRepository.GetOrder(id);

            _orderRepository.EnsureOrderExists(order);

            _orderRepository.ChangeOrderStatus(order, orderStatus);
        }

        private void EnsureOrderHasPendingStatus(Order order)
        {
            if (order.Status != OrderStatusEnum.Pending)
            {
                throw new BadRequestException($"Brak możliwości edytowania zamówienia. Obecny status zamówienia: " +
                    $"{OrderStatusDictionary.OrderStatusesWithDescription.FirstOrDefault(x => x.Key == (byte)order.Status).Value}");
            }
        }
    }
}
