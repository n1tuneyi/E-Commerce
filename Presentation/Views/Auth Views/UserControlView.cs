namespace Ecommerce.Views.AuthViews;

public class UserControlView
{
    public static void ShowUserMenuControl()
    {
        Console.Clear();
        Console.WriteLine("CONTROL MENU");
        Console.WriteLine(new string('-', count: 10));

        Console.WriteLine("1- Browse Products");
        Console.WriteLine("2- View Order History");
        Console.WriteLine("3- Log out");

        Console.Write("Choose one of the options above: ");
    }

    public static int GetMenuSelection()
    {
        int choice;

        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            Console.Write("Invalid choice. Please select a valid choice: ");

        return choice;

    }
}
