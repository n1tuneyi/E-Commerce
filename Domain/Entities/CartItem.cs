namespace Ecommerce.Domain.Entities;
public class CartItem
{
    public long ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public Product Product { get; set; }
}
