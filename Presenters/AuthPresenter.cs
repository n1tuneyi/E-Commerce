using Ecommerce.Domain;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Ecommerce.Views;
using Ecommerce.Views.AuthViews;

namespace Ecommerce.Presenters;

public class AuthPresenter
{
    public static void Login()
    {
        AuthView.ShowLoginPrompt();
        string username = IView.GetInput("Please enter your username: ");
        string password = IView.GetInput("Please enter your password: ");

        try
        {
            UserRepository.currentUser = AuthenticationService.Login(username, password);

            if (UserRepository.currentUser.Username == "admin")
                AdminPresenter.AdminControlMenu();

            else
                UserPresenter.UserControlMenu();

        }

        catch (ArgumentException e)
        {
            Console.WriteLine($"Error Occurred: {e.Message}");
            Thread.Sleep(1000);
            Login();
        }
    }

    public static void Signup()
    {
        string? username, password, email, address;

        username = IView.GetInput("Please Enter your Username: ");

        password = IView.GetInput("Please Enter your Password: ");

        email = IView.GetInput("Please Enter your Email: ");

        address = IView.GetInput("Please Enter your Address: ");

        User user = new User()
        {
            Username = username,
            Password = password,
            Email = email,
            Address = address
        };

        try
        {
            UserRepository.currentUser = UserRepository.Create(user);

            UserPresenter.UserControlMenu();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Occurred: {e.Message}");
            Thread.Sleep(1500);
            Signup();
        }

    }
}
