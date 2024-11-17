using Ecommerce.Domain.Entities;
using Ecommerce.Services;
using Ecommerce.Views;
using Presentation.Authentication;

namespace Ecommerce.Presenters;

public class OrderPresenter
{
    private readonly OrderService _orderService;

    public OrderPresenter(OrderService orderService)
    {
        _orderService = orderService;
    }

    public void OrderHistory()
    {
        List<Order> orders = _orderService.GetOrders(userID: UserSession.CurrentUser.Id);

        if (orders.Count == 0)
            View.Notify("You haven't placed orders yet.");

        else
            OrderView.ShowOrderHistory(orders);
    }

    public void Checkout()
    {
        CheckoutView.ShowCheckoutMenu();

        int choice = CheckoutView.GetMenuSelection();

        switch (choice)
        {
            case 1:
                PlaceOrder();
                break;
        }
    }

    public void PlaceOrder()
    {
        CheckoutView.ShowProcessingOrderMessage();
        _orderService.PlaceOrder(userID: UserSession.CurrentUser.Id);
        CheckoutView.ShowSuccessfulCheckoutMessage();
    }


}
