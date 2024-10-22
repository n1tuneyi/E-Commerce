using Ecommerce.Domain;
using Ecommerce.Repositories;

namespace Ecommerce.Services;

public static class ProductService
{
    public static List<Product> GetProducts()
    {
        return ProductRepository.GetAll();
    }

    public static Product Add(Product product)
    {
        return ProductRepository.Create(product);
    }

    public static bool Exists(long id)
    {
        return ProductRepository.FindById(id) is not null;
    }

    public static bool HasEnoughStock(long id, int requestedQuantity)
    {
        int? quantityInCart = CartService.GetCartItems(UserRepository.currentUser.Id)
                                        ?.Find(item => item.ProductId == id)
                                        ?.Quantity;

        if (quantityInCart is null) { quantityInCart = 0; }

        return quantityInCart + requestedQuantity <= ProductRepository.FindById(id).StockQuantity;
    }

    public static void Remove(long productId)
    {
        ProductRepository.RemoveFromStock(productId, int.MaxValue);
    }

    public static Product? Get(long productId)
    {
        return ProductRepository.FindById(productId);
    }

    public static void RemoveProduct(long productId)
    {
        ProductRepository.Remove(productId);
    }

    public static void UpdateName(long productId, string newName)
    {
        Product product = ProductRepository.FindById(productId);
        product.Name = newName;
    }
    public static void UpdateDescription(long productId, string newDescription)
    {
        Product product = ProductRepository.FindById(productId);
        product.Description = newDescription;
    }
    public static void UpdatePrice(long productId, decimal newPrice)
    {
        Product product = ProductRepository.FindById(productId);
        product.Price = newPrice;
    }

    public static void UpdateQuantity(long productId, int newQuantity)
    {
        Product product = ProductRepository.FindById(productId);
        product.StockQuantity = newQuantity;
    }
}
