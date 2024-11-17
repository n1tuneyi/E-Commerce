namespace Ecommerce.Views.AuthViews;

public class AuthView : View
{
    public static void ShowLoginPrompt()
    {
        Console.Clear();
        Console.WriteLine("LOGIN MENU");
        Console.WriteLine(new string('-', count: 15));
    }

    public static void ShowSignupPrompt()
    {
        Console.Clear();
        Console.WriteLine("SIGNUP MENU");
        Console.WriteLine(new string('-', count: 15));
    }
}
