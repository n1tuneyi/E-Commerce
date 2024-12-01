using Application.DTOs.Cart;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Extensions;

namespace Presentation.WebApi.Controllers
{
    [Route("api/carts")]
    [ApiController]
    [Authorize]

    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCart()
        {
            var cart = await _cartService.GetCartAsync(User.GetUserId());

            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItemToCart(CreateCartItemDTO item)
        {
            await _cartService.AddToCartAsync(item, User.GetUserId());

            return Ok();
        }

        [HttpDelete("items/{prodId}")]
        public async Task<IActionResult> RemoveItemFromCart(Guid prodId)
        {
            await _cartService.RemoveFromCartAsync(prodId, User.GetUserId());

            return NoContent();
        }
    }
}
