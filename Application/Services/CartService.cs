using Application.DTOs.Cart;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Domain.Errors;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services;

public class CartService
{
    private readonly ICartRepository _cartRepository;

    private readonly ILoggerService _loggerService;

    private readonly ProductService _productService;

    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository,
        ILoggerService loggerService,
        ProductService productService,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _loggerService = loggerService;
        _productService = productService;
        _mapper = mapper;
    }


    // Initialize an empty cart in DB after user signsup
    public async Task InitCartAsync(string userId)
    {
        await _cartRepository.CreateAsync(new ShoppingCart() { UserId = userId });
    }

    public async Task<CartDTO> GetCartAsync(string userID)
    {
        ShoppingCart cart = await _cartRepository.GetCartAsync(userID, trackChanges: true);

        CartDTO cartDTO = _mapper.Map<CartDTO>(cart);

        return cartDTO;
    }

    public async Task AddToCartAsync(CreateCartItemDTO item, string userId)
    {
        Product? itemProd = await _productService.GetProductAsync(item.ProductId);

        decimal TotalPrice = item.Quantity * itemProd.Price;

        CartItem newItem = new CartItem()
        {
            Quantity = item.Quantity,
            TotalPrice = TotalPrice,
            Product = itemProd
        };

        ShoppingCart updatedCart = await _cartRepository.GetCartAsync(userId, trackChanges: true);

        CartItem? existingItem = updatedCart.Items.Find(it => it.ProductId == item.ProductId);

        if (existingItem is null)
            updatedCart.Items.Add(newItem);

        else
        {
            existingItem.Quantity += item.Quantity;
            existingItem.TotalPrice += TotalPrice;
            existingItem.UpdatedDate = DateTime.Now;
        }

        updatedCart.TotalPrice += TotalPrice;

        await _cartRepository.UpdateAsync(updatedCart);


        _loggerService.LogInformation($"User#{userId} added {item.Quantity} " +
                        $"amount of Product#{item.ProductId} with total price of {TotalPrice:C}");
    }

    public async Task RemoveFromCartAsync(Guid prodID, string userID)
    {
        ShoppingCart userCart = await _cartRepository.GetCartAsync(userID, trackChanges: true);

        CartItem removedItem = userCart.Items.Find(p => p.ProductId == prodID);

        if (removedItem is null)
        {
            // Throwing Exception is not good here 
            // Because the global error middleware will see this as 500 Internal server error
            // and it's a BadRequest 400 error
            throw new Exception("No such item in the cart");
        }

        userCart.TotalPrice -= removedItem.TotalPrice;

        _loggerService.LogInformation($"User#{userID} removed Product#{removedItem.ProductId} from his cart");

        await _cartRepository.RemoveItemAsync(removedItem, userCart);
    }

    public async Task<CartItem> UpdateItemAsync(Guid prodId, int quantity, string userID)
    {
        if (quantity <= 0)
            throw new InvalidRangeBadRequestException();

        ShoppingCart userCart = await _cartRepository.GetCartAsync(userID, trackChanges: true);

        CartItem? updatedItem = userCart.Items.Find(item => item.ProductId == prodId);

        if (updatedItem is null)
            throw new ItemNotFoundException(prodId);

        Product itemProduct = await _productService.GetProductAsync(prodId);

        userCart.TotalPrice -= updatedItem.TotalPrice;

        updatedItem.Quantity = quantity;

        updatedItem.TotalPrice = updatedItem.Quantity * itemProduct.Price;

        userCart.TotalPrice += updatedItem.TotalPrice;

        updatedItem.UpdatedDate = DateTime.Now;

        _loggerService.LogInformation($"User#{userID} Changed Quantity of Item#{updatedItem.ProductId} " +
                    $"from {updatedItem.Quantity} to {quantity}");

        await _cartRepository.UpdateAsync(userCart);

        return updatedItem;
    }

    public async Task ClearCart(string userId)
    {
        var cart = await _cartRepository.FindByCondition(c => c.UserId == userId, true).SingleAsync();
        cart.Items.Clear();
        cart.TotalPrice = 0;
    }


    //public bool CanAddToCart(long prodID, int requestedQuantity, string userID)
    //{
    //    int? quantityInCart = GetCartItems(userID)
    //                 ?.Find(item => item.ProductId == prodID)
    //                 ?.Quantity;

    //    if (quantityInCart is null) { quantityInCart = 0; }

    //    return quantityInCart + requestedQuantity <= _productService.FindById(prodID).StockQuantity;
    //}

    //public List<CartItem> GetCartItems(string userID)
    //{
    //    return _cartRepository.GetByUserId(userID, trackChanges: true).Items;
    //}

    //public bool HasInCart(long prodID, string userID)
    //{
    //    return GetCartItems(userID)?.FirstOrDefault(item => item.ProductId == prodID) is not null;
    //}
}
