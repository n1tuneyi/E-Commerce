using Ecommerce.Views.AuthViews;

namespace Ecommerce.Presenters;

public class UserPresenter
{
    public static void UserControlMenu()
    {
        UserControlView.ShowUserMenuControl();
        int choice = UserControlView.GetMenuSelection();

        switch (choice)
        {
            case 1:
                ProductPresenter.ProductsMenu();
                CartPresenter.CartOptions();
                break;

            case 2:
                OrderPresenter.OrderHistory();
                break;

            case 3:
                MainPresenter.MainMenu();
                return;
        }

        UserControlMenu();
    }

}
