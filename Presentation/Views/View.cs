namespace Ecommerce.Views;

public abstract class View
{
    public static string GetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    public static void Notify(string message)
    {
        Console.WriteLine();
        Console.WriteLine(message);
        Thread.Sleep(1000);
    }
}
