namespace Ecommerce.Domain.Entities;

public class Order : BaseEntity<long>
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime Date { get; set; }

    public List<OrderItem> Items { get; set; }

    public decimal TotalAmount { get; set; }
}
