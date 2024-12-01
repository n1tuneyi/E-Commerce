using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Extensions;

namespace Presentation.WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]

        public async Task<IActionResult> GetMyOrders()
        {
            var orders = await _orderService.GetOrdersAsync(User.GetUserId());

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var order = await _orderService.PlaceOrderAsync(User.GetUserId());

            return Ok(order);
        }

    }
}
