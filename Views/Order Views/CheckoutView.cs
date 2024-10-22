namespace Ecommerce.Views;

public class CheckoutView
{

    public static void ShowCheckoutMenu()
    {
        Console.WriteLine();
        Console.WriteLine("CHECKOUT MENU");
        Console.WriteLine(new string('-', count: 10));

        Console.WriteLine("1- Proceed to Checkout");
        Console.WriteLine("2- Back to Previous Menu");
        Console.Write("Please choose one of the options above: ");
    }
    public static int GetMenuSelection()
    {
        int choice;

        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
            Console.Write("Invalid choice. Please select a valid choice: ");

        return choice;
    }

    public static void ShowProcessingOrderMessage()
    {
        IView.Notify("Order is being processed ...");
    }
    public static void ShowSuccessfulCheckoutMessage()
    {
        IView.Notify("Order successfully created!");
    }
}
