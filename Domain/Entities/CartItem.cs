namespace Ecommerce.Domain.Entities;
public class CartItem : BaseEntity<long>
{
    public long Id { get; set; }
    public long ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public Product Product { get; set; }
}
