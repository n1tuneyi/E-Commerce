using Application.Repositories;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Services;

public class CartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public void AddToCart(CartItem item, long userId)
    {
        item.TotalPrice = item.Quantity * item.Product.Price;

        if (!_cartRepository.Exists(userId))
        {
            _cartRepository.Create(new ShoppingCart()
            {
                UserId = userId,
                Items = new List<CartItem>() { item },
                TotalPrice = item.TotalPrice,
            });
        }
        else
        {
            ShoppingCart cart = _cartRepository.FindByUserId(userId)!;
            CartItem? existingItem = cart.Items.Find(it => it.ProductId == item.ProductId);

            if (existingItem is null)
                cart.Items.Add(item);

            else
                existingItem.Quantity += item.Quantity;

            cart.TotalPrice += item.TotalPrice;
        }
    }

    public void RemoveFromCart(long prodID, long userID)
    {
        ShoppingCart userCart = _cartRepository.FindByUserId(userID)!;

        userCart.TotalPrice -= userCart.Items.Find(p => p.ProductId == prodID).TotalPrice;

        _cartRepository.RemoveItem(prodID, userID);
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

        CartItem? updatedItem = GetCartItems(userID).FirstOrDefault(item => item.ProductId == prodID);

        userCart.TotalPrice -= updatedItem.TotalPrice;

        updatedItem.Quantity = quantity;

        updatedItem.TotalPrice = updatedItem.Quantity * _productRepository.FindById(prodID).Price;

        userCart.TotalPrice += updatedItem.TotalPrice;

        return updatedItem;
    }

    public bool CanAddToCart(long prodID, int requestedQuantity, long userID)
    {
        int? quantityInCart = GetCartItems(userID)
                     ?.Find(item => item.ProductId == prodID)
                     ?.Quantity;

        if (quantityInCart is null) { quantityInCart = 0; }

        return quantityInCart + requestedQuantity <= _productRepository.FindById(prodID).StockQuantity;
    }
}
