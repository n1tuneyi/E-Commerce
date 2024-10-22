namespace Ecommerce.Domain;
public class CartItem
{
    public long ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
}
