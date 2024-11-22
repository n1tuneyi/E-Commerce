using Application.DTOs;
using Application.Interfaces;
using Application.Repositories;
using Ecommerce.Domain.Entities;

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

    public OrderDTO PlaceOrder(long userID)
    {
        CartDTO cart = _cartService.GetByUserId(userID);

        if (cart.Items.Count == 0)
            throw new Exception("No Items in cart");

        List<OrderItem> orderItems = cart.Items.Select(cartItem => new OrderItem()
        {
            ProductID = cartItem.ProductId,
            Quantity = cartItem.Quantity,
            TotalPrice = cartItem.TotalPrice,
        }).ToList();

        Order order = new Order()
        {
            UserId = userID,
            Date = DateTime.Now,
            Items = orderItems,
            TotalAmount = cart.TotalPrice
        };

        _loggerService.LogInformation($"UserID {order.CreatedBy} placed an order of {order.Items.Count} items and cost {order.TotalAmount:C}");

        _orderRepository.Create(order);

        var orderItemsDto = orderItems.Select(oi => new OrderItemDTO()
        {
            ProductId = oi.ProductID,
            ProductName = oi.Product.Name,
            Quantity = oi.Quantity,
            Price = oi.TotalPrice
        }).ToList();

        var orderDto = new OrderDTO()
        {
            Id = order.Id,
            Items = orderItemsDto,
            OrderDate = order.Date,
            TotalAmount = order.TotalAmount
        };

        foreach (var item in cart.Items)
        {
            _productService.ConsumeProductStock(item.ProductId, item.Quantity);
            _cartService.RemoveFromCart(item.ProductId, 1); // User should be different ID than 1
        }

        return orderDto;
    }

    public IEnumerable<OrderDTO> GetOrders(long userID)
    {
        var orders = _orderRepository.GetOrders(userID, trackChanges: false);

        if (orders.Count() == 0)
            throw new Exception("No Orders yet.");


        var ordersDto = orders.Select(o => new OrderDTO()
        {
            Id = o.Id,
            OrderDate = o.Date,
            TotalAmount = o.TotalAmount,
            Items = o.Items.Select(item => new OrderItemDTO()
            {
                ProductId = item.ProductID,
                ProductName = item.Product.Name,
                Price = item.TotalPrice,
                Quantity = item.Quantity
            }).ToList()
        });

        return ordersDto;
    }
}
