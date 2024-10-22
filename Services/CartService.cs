using Ecommerce.Domain;
using Ecommerce.Repositories;

namespace Ecommerce.Services;


public static class CartService
{
    public static void AddToCart(CartItem item, long userId)
    {
        item.TotalPrice = item.Quantity * ProductRepository.FindById(item.ProductId).Price;

        if (!CartRepository.Exists(userId))
        {
            CartRepository.Create(new ShoppingCart()
            {
                UserId = userId,
                Items = new List<CartItem>() { item },
                TotalPrice = item.TotalPrice,
            });
        }
        else
        {
            ShoppingCart cart = CartRepository.FindByUserId(userId)!;
            CartItem? existingItem = cart.Items.Find(it => it.ProductId == item.ProductId);

            if (existingItem is null)
                cart.Items.Add(item);

            else
                existingItem.Quantity += item.Quantity;

            cart.TotalPrice += item.TotalPrice;
        }

    }

    public static void RemoveFromCart(long prodID, long userID)
    {
        ShoppingCart userCart = CartRepository.FindByUserId(userID)!;

        userCart.TotalPrice -= userCart.Items.Find(p => p.ProductId == prodID).TotalPrice;

        CartRepository.RemoveItem(prodID, userID);
    }

    public static List<CartItem> GetCartItems(long userID)
    {
        return CartRepository.FindByUserId(userID)?.Items!;
    }

    public static bool HasInCart(long prodID)
    {
        return GetCartItems(UserRepository.currentUser.Id)?.FirstOrDefault(item => item.ProductId == prodID) is not null;
    }
}
