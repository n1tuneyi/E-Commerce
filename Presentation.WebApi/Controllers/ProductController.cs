using Application.DTOs.Product;
using Domain.Request.Product;
using Ecommerce.Domain.Entities;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Presentation.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var addedProduct = await _productService.CreateAsync(product);

            return Ok(addedProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParameters productParameters)
        {
            var pagedResult = await _productService.GetProductsAsync(productParameters);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProductAsync(id);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.RemoveAsync(id);

            return NoContent();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductUpdateDto productToUpdate)
        {
            await _productService.UpdateProductAsync(id, productToUpdate);

            return Ok();
        }
    }
}
