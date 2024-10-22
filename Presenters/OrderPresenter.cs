using Ecommerce.Domain;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Ecommerce.Views;

namespace Ecommerce.Presenters;

public class OrderPresenter
{
    public static void OrderHistory()
    {
        List<Order> orders = OrderService.GetOrders(UserRepository.currentUser.Id);

        if (orders.Count == 0)
            IView.Notify("You haven't placed orders yet.");

        else
            OrderView.ShowOrderHistory(orders);
    }

    public static void Checkout()
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

    public static void PlaceOrder()
    {
        CheckoutView.ShowProcessingOrderMessage();
        OrderService.PlaceOrder(UserRepository.currentUser.Id);
        CheckoutView.ShowSuccessfulCheckoutMessage();
    }


}
