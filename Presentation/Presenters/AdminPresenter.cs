using Ecommerce.Services;
using Ecommerce.Views.AuthViews;

namespace Ecommerce.Presenters;

public class AdminPresenter
{
    private readonly ProductPresenter _productPresenter;
    private readonly StockPresenter _stockPresenter;

    private readonly AuthenticationService _authService;
    public AdminPresenter(ProductPresenter productPresenter, StockPresenter stockPresenter, AuthenticationService authService)
    {
        _productPresenter = productPresenter;
        _stockPresenter = stockPresenter;
        _authService = authService;

    }

    public void AdminControlMenu()
    {
        AdminControlView.ShowAdminMenuControl();
        int choice = AdminControlView.GetMenuSelection();

        switch (choice)
        {
            case 1:
                _productPresenter.ProductsMenu();
                _stockPresenter.StockOptions();
                AdminControlMenu();
                break;

            case 2:
                _authService.Logout();
                return;
        }
    }
}