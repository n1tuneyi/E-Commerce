using Application.DTOs.Cart;
using Application.DTOs.Order;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Domain.Errors;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Services;
public class OrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly ProductService _productService;
    private readonly CartService _cartService;

    private readonly ILoggerService _loggerService;

    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository,
                        ProductService productService,
                        CartService cartService,
                        ILoggerService loggerService,
                        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productService = productService;
        _cartService = cartService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<OrderDTO> PlaceOrderAsync(string userId)
    {
        CartDTO cart = await _cartService.GetCartAsync(userId);

        if (cart.Items.Count == 0)
            throw new NoCartItemsFoundException();

        List<OrderItem> orderItems = _mapper.Map<List<OrderItem>>(cart.Items);

        Order order = new Order()
        {
            UserId = userId,
            Date = DateTime.Now,
            Items = orderItems,
            TotalAmount = cart.TotalPrice
        };

        _loggerService.LogInformation($"UserID {order.CreatedBy} placed an order of {order.Items.Count} items and cost {order.TotalAmount:C}");

        await _orderRepository.CreateAsync(order);

        _productService.DeductStock(orderItems);

        var orderDto = _mapper.Map<OrderDTO>(order);

        await _cartService.ClearCart(userId);

        await _orderRepository.Save();

        return orderDto;
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersAsync(string userId)
    {
        var orders = await _orderRepository.GetOrdersAsync(userId, trackChanges: false);

        if (orders.Count() == 0)
            throw new NoOrdersFoundException();

        var ordersDto = _mapper.Map<IEnumerable<OrderDTO>>(orders);

        return ordersDto;
    }
}
