using Ecommerce.Domain;
using Ecommerce.Services;
using Ecommerce.Views;

namespace Ecommerce.Presenters;

public class ProductPresenter
{
    public static void ProductsMenu()
    {
        List<Product> availableProducts = ProductService.GetProducts();

        ProductView.ShowAvailableProducts(availableProducts);
    }
}
