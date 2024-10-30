using Application.Repositories;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Services;
public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public void PlaceOrder(long userID)
    {
        ShoppingCart? cart = _cartRepository.FindByUserId(userID);

        Order order = new Order()
        {
            UserId = userID,
            Date = DateTime.Now,
            Items = cart.Items,
            TotalAmount = cart.TotalPrice
        };

        _orderRepository.Create(order);

        foreach (CartItem item in cart.Items)
        {
            _productRepository.RemoveFromStock(item.ProductId, item.Quantity);
        }

        _cartRepository.Remove(cart);
    }

    public List<Order> GetOrders(long userID)
    {
        return _orderRepository.GetOrdersByUserId(userID);
    }
}
