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
        private readonly IOrderService _orderService;
  
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetOrderStatuses")]
        public IActionResult GetOrderStatuses()
        {
            return Ok(OrderStatusDictionary.OrderStatusesWithDescription);
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders([FromQuery] List<OrderStatusEnum> orderStatuses, [FromQuery] long userId = 0, [FromQuery] string orderByParams = null)
        {
            return Ok(await _orderService.GetOrders(orderStatuses, userId, orderByParams));
        }

        [HttpGet("GetOrdersHistory")]
        public async Task<IActionResult> GetOrdersHistory([FromQuery] long userId = 0, [FromQuery] string orderByParams = null)
        {
            return Ok(await _orderService.GetOrdersHistory(userId, orderByParams));
        }

        [HttpGet("GetOrdersForAdminPanel")]
        public async Task<IActionResult> GetOrdersForAdminPanel([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string orderByParams = null)
        {
            return Ok(await _orderService.GetOrdersForAdminPanel(pageIndex, pageSize, orderByParams));
        }

        [HttpGet("GetOrderById/{id}")]
        public IActionResult GetOrderById([FromRoute] long id)
        {
            return Ok(_orderService.GetOrder(id));
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
    }
}
