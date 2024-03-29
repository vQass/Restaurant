﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Authentication.Attributes;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.OrderModels;
using Restaurant.Entities.Enums;

namespace Restaurant.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("orders/statuses")]
        [Authorize]
        public IActionResult GetOrderStatuses()
        {
            return Ok(_orderService.GetOrderStatuses());
        }

        [HttpGet("orders/historyPage")]
        [Authorize]
        public async Task<IActionResult> GetOrdersHistory(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 0,
            [FromQuery] long userId = 0,
            [FromQuery] string orderByParams = null)
        {
            return Ok(await _orderService.GetOrdersHistory(pageIndex, pageSize, userId, orderByParams));
        }

        [HttpGet("orders/page")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public async Task<IActionResult> GetOrdersPage([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string orderByParams = null)
        {
            return Ok(await _orderService.GetOrdersForAdminPanel(pageIndex, pageSize, orderByParams));
        }

        [HttpPost("orders")]
        [Authorize()]
        public IActionResult AddOrder(OrderCreateRequest orderCreateRequest)
        {
            var id = _orderService.AddOrder(orderCreateRequest);
            return Created($"GetOrder/{id}", null);
        }

        [HttpPatch("orders/changeStatus/{id}")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public IActionResult ChangeOrderStatus(long id, [FromBody] OrderStatusEnum orderStatus)
        {
            _orderService.ChangeOrderStatus(id, orderStatus);
            return Ok();
        }
    }
}
