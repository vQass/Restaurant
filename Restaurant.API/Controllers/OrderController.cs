using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models.OrderModels;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;
using Restaurant.IServices;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Fields

        private readonly IOrderService _orderService;

        #endregion

        #region Ctors

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        #region Methods

        [HttpGet("GetOrderStatuses")]
        public IActionResult GetOrderStatuses()
        {
            return Ok(OrderStatusDictionary.OrderStatusesWithDescription);
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders([FromQuery] List<OrderStatusEnum> orderStatuses)
        {
            return Ok(await _orderService.GetOrders(orderStatuses));
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] long id)
        {
            return Ok(_orderService.GetOrderById(id));
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrder(OrderCreateRequest orderCreateRequest)
        {
            var id = _orderService.AddOrder(orderCreateRequest);
            return Created($"GetOrder/{id}", null); 
        }

        [HttpPatch("ChangeOrderStatus/{id}")]
        public IActionResult ChangeOrderStatus(long id,OrderStatusEnum orderStatus)
        {
            _orderService.ChangeOrderStatus(id, orderStatus);
            return Ok();
        }

        #endregion
    }
}
