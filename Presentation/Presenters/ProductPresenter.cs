using Ecommerce.Domain.Entities;
using Ecommerce.Services;
using Ecommerce.Views;

namespace Ecommerce.Presenters;

public class ProductPresenter
{
    private readonly ProductService _productService;

    public ProductPresenter(ProductService productService)
    {
        _productService = productService;
    }

    public void ProductsMenu()
    {
        IEnumerable<Product> availableProducts = _productService.GetProducts();

        ProductView.ShowAvailableProducts(availableProducts);
    }
}
