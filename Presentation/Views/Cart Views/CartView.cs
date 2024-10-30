using Ecommerce.Domain.Entities;

namespace Ecommerce.Views;

public class CartView
{
    public static void ShowCartOptions()
    {
        Console.WriteLine();
        Console.WriteLine("1- View Cart");
        Console.WriteLine("2- Add Product to Cart");
        Console.WriteLine("3- Remove Products from Cart");
        Console.WriteLine("4- Update Cart");
        Console.WriteLine("5- Back to Control Menu");
        Console.Write("Choose one of the options above: ");
    }

    public static int GetMenuSelection()
    {
        int choice;

        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
            Console.Write("Invalid choice. Please select a valid choice: ");

        return choice;
    }

    public static void ShowCart(ShoppingCart cart)
    {
        Console.WriteLine("Your Cart");
        Console.WriteLine(new string('-', count: 12));

        DisplayItems(cart.Items);

        Console.WriteLine($"Total Amount to pay is {cart.TotalPrice:C}");
    }

    public static void ShowEmptyCart()
    {
        IView.Notify("Your Cart is empty!");
    }

    public static void DisplayItems(List<CartItem> items)
    {
        foreach (CartItem item in items)
        {
            Console.WriteLine($"ID: {item.Product.Id}");
            Console.WriteLine($"Product name: {item.Product.Name}");
            Console.WriteLine($"Quantity: {item.Quantity}");
            Console.WriteLine($"Total Price: {item.TotalPrice:C}");
            Console.WriteLine(new string('-', count: 10));
        }
    }

    public static string GetProductIDToAdd()
    {
        Console.Write("Type the ID of product you want to add to your cart or Enter -1 to return to the previous menu: ");
        return Console.ReadLine();
    }

    public static string GetProductIDToRemove()
    {
        Console.Write("Type the ID of product you want to remove from your cart or Enter -1 to return to the previous menu: ");
        return Console.ReadLine();
    }
    public static string GetProductIDToUpdate()
    {
        Console.Write("Type the ID of product you want to change in cart or Enter -1 to return to previous menu: ");
        return Console.ReadLine();
    }

    public static void ShowSuccessfulAddedToCartMessage()
    {
        IView.Notify("Item added succesfully to your cart!");
    }

    public static void ShowSuccessfulUpdatedItemMessage()
    {
        IView.Notify("Item updated succesfully!");
    }

    public static void ShowSuccessfulRemovedFromCartMessage()
    {
        IView.Notify("Item removed successfully from your cart");
    }
}
