using Ecommerce.Views;

namespace Ecommerce.Presenters;

public class MainPresenter
{
    private readonly AuthPresenter _authPresenter;

    public MainPresenter(AuthPresenter authPresenter)
    {
        _authPresenter = authPresenter;
    }

    public void MainMenu()
    {
        while (true)
        {
            MainView.ShowMainMenu();
            int choice = MainView.GetMenuSelection();

            switch (choice)
            {
                case 1:
                    _authPresenter.Login();
                    break;

                case 2:
                    _authPresenter.Signup();
                    break;

                case 3:
                    IView.Notify("Exiting Application ...");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
