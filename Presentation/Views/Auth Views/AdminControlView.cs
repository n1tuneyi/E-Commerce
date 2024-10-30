namespace Ecommerce.Views.AuthViews;

public class AdminControlView
{
    public static void ShowAdminMenuControl()
    {
        Console.Clear();
        Console.WriteLine("CONTROL MENU");
        Console.WriteLine(new string('-', count: 10));

        Console.WriteLine("1- View Current Products");
        Console.WriteLine("2- Log out");

        Console.Write("Choose one of the options above: ");
    }

    public static int GetMenuSelection()
    {
        int choice;

        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
            Console.Write("Invalid choice. Please select a valid choice: ");

        return choice;

    }
}
