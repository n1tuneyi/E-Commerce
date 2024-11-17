using Ecommerce.Services;
using Ecommerce.Views.AuthViews;

namespace Ecommerce.Presenters;

public class UserPresenter
{
    private readonly ProductPresenter _productPresenter;
    private readonly CartPresenter _cartPresenter;
    private readonly OrderPresenter _orderPresenter;
    private readonly AuthenticationService _authService;

    public UserPresenter(ProductPresenter productPresenter,
                         CartPresenter cartPresenter,
                         OrderPresenter orderPresenter,
                         AuthenticationService authService)
    {
        _productPresenter = productPresenter;
        _cartPresenter = cartPresenter;
        _orderPresenter = orderPresenter;
        _authService = authService;
    }

    public void UserControlMenu()
    {
        UserControlView.ShowUserMenuControl();
        int choice = UserControlView.GetMenuSelection();

        switch (choice)
        {
            case 1:
                _productPresenter.ProductsMenu();
                _cartPresenter.CartOptions();
                break;

            case 2:
                _orderPresenter.OrderHistory();
                break;

            case 3:
                _authService.Logout();
                return;
        }

        UserControlMenu();
    }
}
