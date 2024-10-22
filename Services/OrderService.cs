using Ecommerce.Domain;
using Ecommerce.Repositories;

namespace Ecommerce.Services;
public static class OrderService
{
    public static void PlaceOrder(long userID)
    {
        ShoppingCart? cart = CartRepository.FindByUserId(userID);

        Order order = new Order()
        {
            UserId = userID,
            Date = DateTime.Now,
            Items = cart.Items,
            TotalAmount = cart.TotalPrice
        };

        OrderRepository.Create(order);

        foreach (CartItem item in cart.Items)
        {
            ProductRepository.RemoveFromStock(item.ProductId, item.Quantity);
        }

        CartRepository.RemoveCart(cart);
    }

    public static List<Order> GetOrders(long userID)
    {
        return OrderRepository.GetAll(userID);
    }
}
