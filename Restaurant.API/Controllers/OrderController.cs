using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetOrderStatuses()
        {
            return Ok(_orderService.GetOrderStatuses());
        }

        [HttpGet("orders/historyPage")]
        public async Task<IActionResult> GetOrdersHistory(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 0,
            [FromQuery] long userId = 0,
            [FromQuery] string orderByParams = null)
        {
            return Ok(await _orderService.GetOrdersHistory(pageIndex, pageSize, userId, orderByParams));
        }

        [HttpGet("orders/page")]
        public async Task<IActionResult> GetOrdersForAdminPanel([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string orderByParams = null)
        {
            return Ok(await _orderService.GetOrdersForAdminPanel(pageIndex, pageSize, orderByParams));
        }

        [HttpPost("orders")]
        public IActionResult AddOrder(OrderCreateRequest orderCreateRequest)
        {
            var id = _orderService.AddOrder(orderCreateRequest);
            return Created($"GetOrder/{id}", null);
        }

        [HttpPatch("orders/changeStatus/{id}")]
        public IActionResult ChangeOrderStatus(long id, [FromBody] OrderStatusEnum orderStatus)
        {
            _orderService.ChangeOrderStatus(id, orderStatus);
            return Ok();
        }
    }
}
