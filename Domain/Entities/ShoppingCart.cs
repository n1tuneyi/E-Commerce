namespace Ecommerce.Domain.Entities;
public class ShoppingCart
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
    public decimal TotalPrice { get; set; }
}
