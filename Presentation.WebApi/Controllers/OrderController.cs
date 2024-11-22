using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetMyOrders()
        {
            var orders = _orderService.GetOrders(1);

            return Ok(orders);
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var order = _orderService.PlaceOrder(1);

            return Ok(order);
        }

    }
}
