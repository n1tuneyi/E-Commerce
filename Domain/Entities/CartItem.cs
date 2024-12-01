namespace Ecommerce.Domain.Entities;
public class CartItem : BaseEntity<Guid>
{
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
