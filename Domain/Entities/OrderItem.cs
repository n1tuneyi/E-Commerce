namespace Ecommerce.Domain.Entities;
public class OrderItem : BaseEntity<Guid>
{
    public Guid OrderID { get; set; }
    public Order Order { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}
