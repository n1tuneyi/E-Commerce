using Application.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApi.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult GetMyCart()
        {
            // Later when we do authentication we will know the ID of the connected user
            // but for now we are using the User with ID 1 

            var cart = _cartService.GetByUserId(1);

            return Ok(cart);
        }

        [HttpPost]
        public IActionResult AddItemToCart(CreateCartItemDTO item)
        {
            _cartService.AddToCart(item, 1);

            return Ok();
        }

        [HttpDelete("items/{prodId}")]
        public IActionResult RemoveItemFromCart(long prodId)
        {
            _cartService.RemoveFromCart(prodId, 1);
            return NoContent();
        }


    }
}
