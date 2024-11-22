using Application.Authentication;
using Application.DTOs;
using Application.Interfaces;
using Application.Repositories;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Services;

public class CartService
{
    private readonly ICartRepository _cartRepository;

    private readonly ILoggerService _loggerService;

    private readonly ProductService _productService;

    public CartService(ICartRepository cartRepository,
        ILoggerService loggerService,
        ProductService productService)
    {
        _cartRepository = cartRepository;
        _loggerService = loggerService;
        _productService = productService;
    }

    public CartDTO GetByUserId(long userID)
    {
        ShoppingCart cart = _cartRepository.GetByUserId(userID, trackChanges: true);

        CartDTO cartDTO = new CartDTO()
        {
            Items = cart.Items.Select(item => new ViewCartItemDTO(
                ProductId: item.ProductId,
                Description: item.Product.Description,
                Name: item.Product.Name,
                Price: item.Product.Price,
                Quantity: item.Quantity,
                StockQuantity: item.Product.StockQuantity,
                TotalPrice: item.Product.Price * item.Quantity
            )).ToList(),

            TotalPrice = cart.Items.Sum(item => item.TotalPrice)
        };

        return cartDTO;
    }

    public List<CartItem> GetCartItems(long userID)
    {
        return _cartRepository.GetByUserId(userID, trackChanges: true).Items;
    }

    public bool HasInCart(long prodID, long userID)
    {
        return GetCartItems(userID)?.FirstOrDefault(item => item.ProductId == prodID) is not null;
    }

    public void AddToCart(CreateCartItemDTO item, long userId)
    {
        Product? itemProd = _productService.FindById(item.ProductId);

        decimal TotalPrice = item.Quantity * itemProd.Price;

        CartItem newItem = new CartItem()
        {
            Quantity = item.Quantity,
            TotalPrice = TotalPrice,
            Product = itemProd
        };

        if (!_cartRepository.Exists(userId))
        {
            _cartRepository.Create(new ShoppingCart()
            {
                UserId = userId,
                Items = new List<CartItem>() {
                    newItem
                },
                TotalPrice = newItem.TotalPrice
            });
        }

        else
        {
            ShoppingCart updatedCart = _cartRepository.GetByUserId(userId, trackChanges: true);
            CartItem? existingItem = updatedCart.Items.Find(it => it.ProductId == item.ProductId);

            if (existingItem is null)
                updatedCart.Items.Add(newItem);

            else
            {
                existingItem.Quantity += item.Quantity;
                existingItem.TotalPrice += TotalPrice; // Check this
                //existingItem.UpdatedBy = UserSession.CurrentUser.Id;
                existingItem.UpdatedDate = DateTime.Now;
            }

            updatedCart.TotalPrice += TotalPrice;

            _cartRepository.Update(updatedCart);
        }

        _loggerService.LogInformation($"User#{userId} added {item.Quantity} " +
                        $"amount of Product#{item.ProductId} with total price of {TotalPrice:C}");
    }

    public void RemoveFromCart(long prodID, long userID)
    {
        ShoppingCart userCart = _cartRepository.GetByUserId(userID, trackChanges: true);

        CartItem removedItem = userCart.Items.Find(p => p.ProductId == prodID);

        userCart.TotalPrice -= removedItem.TotalPrice;

        _loggerService.LogInformation($"User#{userID} removed Product#{removedItem.ProductId} from his cart");

        _cartRepository.RemoveItem(removedItem, userCart);
    }


    public CartItem UpdateItem(long prodID, int quantity, long userID)
    {
        ShoppingCart userCart = _cartRepository.GetByUserId(userID, trackChanges: true);

        CartItem? updatedItem = userCart.Items.FirstOrDefault(item => item.ProductId == prodID);

        userCart.TotalPrice -= updatedItem.TotalPrice;

        updatedItem.Quantity = quantity;

        updatedItem.TotalPrice = updatedItem.Quantity * _productService.FindById(prodID).Price;

        userCart.TotalPrice += updatedItem.TotalPrice;

        updatedItem.UpdatedDate = DateTime.Now;

        updatedItem.UpdatedBy = UserSession.CurrentUser.Id;

        _loggerService.LogInformation($"User#{userID} Changed Quantity of Item#{updatedItem.ProductId} " +
                    $"from {updatedItem.Quantity} to {quantity}");

        _cartRepository.Update(userCart);

        return updatedItem;
    }

    public bool CanAddToCart(long prodID, int requestedQuantity, long userID)
    {
        int? quantityInCart = GetCartItems(userID)
                     ?.Find(item => item.ProductId == prodID)
                     ?.Quantity;

        if (quantityInCart is null) { quantityInCart = 0; }

        return quantityInCart + requestedQuantity <= _productService.FindById(prodID).StockQuantity;
    }
}
