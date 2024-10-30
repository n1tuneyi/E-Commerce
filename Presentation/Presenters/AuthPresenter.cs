using Ecommerce.Domain.Entities;
using Ecommerce.Services;
using Ecommerce.Views;
using Ecommerce.Views.AuthViews;
using Presentation.Authentication;

namespace Ecommerce.Presenters;

public class AuthPresenter
{
    private readonly AuthenticationService _authService;

    private readonly AdminPresenter _adminPresenter;
    private readonly UserPresenter _userPresenter;

    public AuthPresenter(AuthenticationService authService, AdminPresenter adminpresenter, UserPresenter userPresenter)
    {
        _authService = authService;
        _adminPresenter = adminpresenter;
        _userPresenter = userPresenter;
    }

    public void Login()
    {
        AuthView.ShowLoginPrompt();
        string username = IView.GetInput("Please enter your username: ");
        string password = IView.GetInput("Please enter your password: ");

        try
        {
            UserSession.CurrentUser = _authService.Login(username, password);

            if (UserSession.CurrentUser.Username == "admin")
                _adminPresenter.AdminControlMenu();

            else
                _userPresenter.UserControlMenu();
        }

        catch (ArgumentException e)
        {
            Console.WriteLine($"Error Occurred: {e.Message}");
            Thread.Sleep(1000);
            Login();
        }
    }

    public void Signup()
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
            UserSession.CurrentUser = _authService.Signup(user);

            _userPresenter.UserControlMenu();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Occurred: {e.Message}");
            Thread.Sleep(1500);
            Signup();
        }

    }
}
