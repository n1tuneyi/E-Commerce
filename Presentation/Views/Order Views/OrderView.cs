using Ecommerce.Domain.Entities;

namespace Ecommerce.Views;

public class OrderView
{
    public static void ShowOrderHistory(List<Order> orders)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("ORDER HISTORY");
        Console.WriteLine(new string('-', count: 10));
        Console.WriteLine();

        foreach (Order order in orders)
        {
            Console.WriteLine($"Order ID: {order.Id}");
            Console.WriteLine($"Order Date: {order.Date}");
            Console.WriteLine();
            Console.WriteLine("Products Bought: ");
            //CartView.DisplayItems(order.Items);
            Console.WriteLine($"Total Amount Paid: {order.TotalAmount:C}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        Console.WriteLine($"Press any key to return to previous menu");
        Console.ReadKey();
    }

}
