using Ecommerce.Domain.Models;

namespace Ecommerce.Repositories;

public static class ProductRepository
{
    private static readonly List<Product> Products = [
        new Product()
        {
            Id = 1,
            Name = "Apples",
            Description = "American Apples with rich Vitamins and good taste",
            Price = 8,
            StockQuantity = 60
        },
        new Product()
        {
            Id = 2,
            Name = "Oranges",
            Description = "Egyptian Oranges",
            Price = 4,
            StockQuantity = 100
        }
    ];

    public static Product Create(Product product)
    {
        product.Id = Products.Count + 1;

        Products.Add(product);

        return product;
    }

    public static Product? FindById(long id)
    {
        return Products.Find(product => product.Id == id);
    }

    public static List<Product> GetAll()
    {
        return Products;
    }

    public static void Remove(long id)
    {
        Product? removedProduct = Products.Find(product => product.Id == id);

        Products.Remove(removedProduct);
    }
    public static void RemoveFromStock(long id, int quantity)
    {
        Product product = Products.Find(product => product.Id == id)!;

        if (product.StockQuantity < quantity)
            throw new ArgumentException($"No enough stock only {product.StockQuantity} left");

        product.StockQuantity -= quantity;
    }

    public static void Update(Product newProduct)
    {
        Product product = Products.Find(product => product.Id == newProduct.Id);


    }
}
