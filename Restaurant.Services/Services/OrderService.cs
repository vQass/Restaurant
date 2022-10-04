﻿using AutoMapper;
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMealRepository _mealRepository;

        public OrderService(
            IMapper mapper,
            IOrderRepository orderRepository,
            IPromotionRepository promotionRepository, 
            ICityRepository cityRepository,
            IMealRepository mealRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _promotionRepository = promotionRepository;
            _cityRepository = cityRepository;
            _mealRepository = mealRepository;
        }


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
        
        public async Task<IEnumerable<OrderHistoryViewModel>> GetOrdersHistory(long userId = 0)
        {
            var orders = await _orderRepository.GetOrders(null, userId);

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
                OrderElements = x.OrderElements
                    .Select(y => new OrderElementViewModel()
                    {
                        Amount = y.Amount,
                        MealName = meals.FirstOrDefault(z => z.Id == y.MealId).Name,
                        Price = y.CurrentPrice
                    })
                    .ToList()
            });

            return ordersHistory;
        }

        public long AddOrder(OrderCreateRequest orderCreateRequest)
        {
            if(orderCreateRequest.PromotionCode != null)
            {
                var promotion = _promotionRepository.GetPromotion(orderCreateRequest.PromotionCode);

                _promotionRepository.EnsurePromotionExists(promotion);

                _promotionRepository.EnsurePromotionIsActive(promotion);

                orderCreateRequest.PromotionId = promotion?.Id;
            }

            FillOrderMealsPrices(orderCreateRequest);

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

        private void FillOrderMealsPrices(OrderCreateRequest orderCreateRequest)
        {
            var meals = _mealRepository.GetMeals();
        }
    }
}
