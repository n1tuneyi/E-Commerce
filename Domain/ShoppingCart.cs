namespace Ecommerce.Domain;
public class ShoppingCart
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public List<CartItem> Items { get; set; }
}
