namespace Ecommerce.Views;

public class MainView
{
    public static void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("MAIN MENU");
        Console.WriteLine(new string('-', count: 10));

        Console.WriteLine("1- Login");
        Console.WriteLine("2- Signup");
        Console.WriteLine("3- Exit the Application");

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
