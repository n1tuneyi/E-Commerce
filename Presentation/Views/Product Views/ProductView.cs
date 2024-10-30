using Ecommerce.Domain.Entities;

namespace Ecommerce.Views;

public class ProductView
{
    public static void ShowAvailableProducts(List<Product> products)
    {
        Console.Clear();
        Console.WriteLine("Available Products");

        if (products.Count == 0)
        {
            Console.WriteLine("There are no available products yet.");
            Thread.Sleep(1000);
        }
        else
        {
            foreach (Product product in products)
            {
                Console.WriteLine(new string('-', count: 10));
                Console.WriteLine($"ID: {product.Id}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Description: {product.Description}");
                Console.WriteLine($"Price: {product.Price:C}");
                Console.WriteLine($"Available Stock: {product.StockQuantity}");
            }
        }
    }
}
