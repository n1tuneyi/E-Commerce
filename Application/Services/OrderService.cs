using Application.Interfaces;
using Application.Repositories;
using Ecommerce.Domain.Entities;
using Presentation.Authentication;

namespace Ecommerce.Services;
public class OrderService
{
    private readonly IOrderRepository _orderRepository;


    private readonly ProductService _productService;
    private readonly CartService _cartService;

    private readonly ILoggerService _loggerService;

    public OrderService(IOrderRepository orderRepository,
                        ProductService productService,
                        CartService cartService,
                        ILoggerService loggerService)
    {
        _orderRepository = orderRepository;
        _productService = productService;
        _cartService = cartService;
        _loggerService = loggerService;
    }

    public void PlaceOrder(long userID)
    {
        ShoppingCart? cart = _cartService.GetByUserId(userID);

        List<OrderItem> orderItems = cart.Items.Select(cartItem => new OrderItem()
        {
            ProductID = cartItem.ProductId,
            Quantity = cartItem.Quantity,
            TotalPrice = cartItem.TotalPrice,
            CreatedBy = UserSession.CurrentUser.Id
        }).ToList();

        Order order = new Order()
        {
            UserId = userID,
            Date = DateTime.Now,
            Items = orderItems,
            TotalAmount = cart.TotalPrice,
            CreatedBy = UserSession.CurrentUser.Id
        };

        _loggerService.LogInformation($"UserID {order.CreatedBy} placed an order of {order.Items.Count} items and cost {order.TotalAmount:C}");

        _orderRepository.Create(order);

        foreach (CartItem item in cart.Items)
        {
            _productService.ConsumeProductStock(item.ProductId, item.Quantity);
            _cartService.RemoveFromCart(item.ProductId, UserSession.CurrentUser.Id);
        }
    }

    public IEnumerable<Order> GetOrders(long userID)
    {
        return _orderRepository.GetOrders(userID, trackChanges: false);
    }
}
