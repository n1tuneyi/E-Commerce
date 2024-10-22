using Ecommerce.Views.AuthViews;

namespace Ecommerce.Presenters;

public class AdminPresenter
{
    public static void AdminControlMenu()
    {
        AdminControlView.ShowAdminMenuControl();
        int choice = AdminControlView.GetMenuSelection();

        switch (choice)
        {
            case 1:
                ProductPresenter.ProductsMenu();
                StockPresenter.StockOptions();
                break;

            case 2:
                MainPresenter.MainMenu();
                break;
        }
    }
}