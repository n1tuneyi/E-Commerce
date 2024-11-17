using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities;
public class ShoppingCart
{
    public long Id { get; set; }
    public long UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public decimal TotalPrice { get; set; }
}
