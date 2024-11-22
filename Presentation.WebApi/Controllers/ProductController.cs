using Ecommerce.Domain.Entities;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            var addedProduct = _productService.Create(product);

            return Ok(addedProduct);
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(long id)
        {
            var product = _productService.FindById(id);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(long id)
        {
            _productService.Remove(id);

            return NoContent();
        }

    }
}
