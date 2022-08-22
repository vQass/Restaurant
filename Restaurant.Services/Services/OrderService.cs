using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.OrderModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;
using Restaurant.IRepository;
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

        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IPromotionRepository _promotionRepository;

        #endregion

        #region Ctors

        public OrderService(
            IMapper mapper,
            IOrderRepository orderRepository,
            IPromotionRepository promotionRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _promotionRepository = promotionRepository;
        }

        #endregion

        #region PublicMethods

        public Order GetOrder(long id)
        {
            var order = _orderRepository.GetOrder(id);

            _orderRepository.EnsureOrderExists(order);

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrders(IEnumerable<OrderStatusEnum> orderStatuses = null, long userId = 0)
        {
            return await _orderRepository.GetOrders(orderStatuses, userId);
        }

        public long AddOrder(OrderCreateRequest orderCreateRequest)
        {
            var promotion = _promotionRepository.GetPromotion(orderCreateRequest.PromotionCode);

            _promotionRepository.EnsurePromotionExists(promotion);

            _promotionRepository.EnsurePromotionIsActive(promotion);

            orderCreateRequest.PromotionId = promotion?.Id;

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

        #endregion

        #region PrivateMethods

        private void EnsureOrderHasPendingStatus(Order order)
        {
            if (order.Status != OrderStatusEnum.Pending)
            {
                throw new BadRequestException($"Brak możliwości edytowania zamówienia. Obecny status zamówienia: " +
                    $"{OrderStatusDictionary.OrderStatusesWithDescription.FirstOrDefault(x => x.Key == (byte)order.Status).Value}");
            }
        }
        #endregion

    }
}
