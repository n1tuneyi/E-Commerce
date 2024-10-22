using Ecommerce.Views;

namespace Ecommerce.Presenters;

public class MainPresenter
{
    public static void MainMenu()
    {
        while (true)
        {
            MainView.ShowMainMenu();
            int choice = MainView.GetMenuSelection();

            switch (choice)
            {
                case 1:
                    AuthPresenter.Login();
                    break;

                case 2:
                    AuthPresenter.Signup();
                    break;

                case 3:
                    Console.WriteLine("Exiting Application ...");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
