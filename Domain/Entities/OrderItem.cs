namespace Ecommerce.Domain.Entities;
public class OrderItem : BaseEntity<long>
{
    public long Id { get; set; }
    public long OrderID { get; set; }
    public Order Order { get; set; }
    public long ProductID { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
}
