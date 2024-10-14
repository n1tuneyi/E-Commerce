using Ecommerce.Domain;
using Ecommerce.Repositories;
using Ecommerce.Services;

namespace Ecommerce;

public class Program
{
    static void Main(string[] args)
    {
        MainMenu();
    }

    static void MainMenu()
    {
        Console.WriteLine("MAIN MENU");
        Console.WriteLine(new string('-', count: 10));

        Console.WriteLine("1- Login");
        Console.WriteLine("2- Signup");

        Console.WriteLine("Choose one of the options above");

        int choice = int.Parse(Console.ReadLine()!);

        switch (choice)
        {
            case 1:
                LoginMenu();
                break;

            case 2:
                SignupMenu();
                break;

            default:
                Console.WriteLine("Invalid Input");
                break;
        }

    }

    static void LoginMenu()
    {
        Console.WriteLine("Please enter your username: ");
        string? username = Console.ReadLine();
        Console.WriteLine("Please enter your password: ");
        string? password = Console.ReadLine();
        try
        {
            User? user = AuthenticationService.Login(username, password);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Error Occurred: {e.Message}");
            LoginMenu();
        }
    }

    static void SignupMenu()
    {
        string? username, password, email, address;

        Console.WriteLine("Please Enter your Username: ");
        username = Console.ReadLine();

        Console.WriteLine("Please Enter your Password: ");
        password = Console.ReadLine();

        Console.WriteLine("Please Enter your Email: ");
        email = Console.ReadLine();

        Console.WriteLine("Please Enter your Address: ");
        address = Console.ReadLine();

        User user = new User()
        {
            Username = username,
            Password = password,
            Email = email,
            Address = address
        };

        try
        {
            UserRepository.Create(user);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Occurred: {e.Message}");
        }

    }

}