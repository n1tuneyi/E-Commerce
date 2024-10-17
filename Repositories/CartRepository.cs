using Ecommerce.Domain;

namespace Ecommerce.Repositories;

public static class CartRepository
{
    private static readonly List<ShoppingCart> carts = new List<ShoppingCart>();

    public static ShoppingCart Create(ShoppingCart cart)
    {
        cart.Id = carts.Count + 1;

        carts.Add(cart);

        return cart;
    }

    public static ShoppingCart? FindByUserId(long id)
    {
        return carts.FirstOrDefault(cart => cart.UserId == id);
    }

    public static void RemoveItem(long prodID, long userID)
    {
        ShoppingCart userCart = carts.FirstOrDefault(cart => cart.UserId == userID)!;

        userCart.Items.RemoveAll(item => item.ProductId == prodID);
    }

    public static void RemoveCart(ShoppingCart cart)
    {
        carts.Remove(cart);
    }

    public static bool Exists(long id)
    {
        return FindByUserId(id) is not null;
    }

}
