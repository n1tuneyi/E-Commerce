using Application.Interfaces;
using Application.Repositories;
using Ecommerce.Domain.Entities;
using Presentation.Authentication;

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

    public void AddToCart(CartItem item, long userId)
    {
        item.Product = _productService.FindById(item.ProductId);

        item.TotalPrice = item.Quantity * item.Product.Price;

        if (!_cartRepository.Exists(userId))
        {
            _cartRepository.Create(new ShoppingCart()
            {
                UserId = userId,
                Items = new List<CartItem>() { item },
                TotalPrice = item.TotalPrice,
            });

            return;
        }

        else
        {
            ShoppingCart updatedCart = _cartRepository.FindByUserId(userId)!;
            CartItem? existingItem = updatedCart.Items.Find(it => it.ProductId == item.ProductId);

            if (existingItem is null)
                updatedCart.Items.Add(item);

            else
            {
                existingItem.Quantity += item.Quantity;
                existingItem.TotalPrice += item.TotalPrice; // Check this
                existingItem.UpdatedBy = UserSession.CurrentUser.Id;
                existingItem.UpdatedDate = DateTime.Now;
            }

            updatedCart.TotalPrice += item.TotalPrice;

            _cartRepository.Update(updatedCart);
        }

        _loggerService.LogInformation($"User#{userId} added {item.Quantity} " +
                        $"amount of Product#{item.ProductId} with total price of {item.TotalPrice:C}");
    }

    public void RemoveFromCart(long prodID, long userID)
    {
        ShoppingCart userCart = _cartRepository.FindByUserId(userID)!;

        CartItem removedItem = userCart.Items.Find(p => p.ProductId == prodID);

        userCart.TotalPrice -= removedItem.TotalPrice;

        _loggerService.LogInformation($"User#{userID} removed Product#{removedItem.ProductId} from his cart");

        _cartRepository.RemoveItem(removedItem, userCart);
    }

    public ShoppingCart GetByUserId(long userID)
    {
        return _cartRepository.FindByUserId(userID);
    }

    public List<CartItem> GetCartItems(long userID)
    {
        return _cartRepository.FindByUserId(userID)?.Items!;
    }

    public bool HasInCart(long prodID, long userID)
    {
        return GetCartItems(userID)?.FirstOrDefault(item => item.ProductId == prodID) is not null;
    }

    public CartItem UpdateItem(long prodID, int quantity, long userID)
    {
        ShoppingCart userCart = _cartRepository.FindByUserId(userID);

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
