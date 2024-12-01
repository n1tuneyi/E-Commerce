namespace Ecommerce.Domain.Entities;

public class Order : BaseEntity<Guid>
{
    public string UserId { get; set; }

    public User User { get; set; }

    public DateTime Date { get; set; }

    public List<OrderItem> Items { get; set; }

    public decimal TotalAmount { get; set; }
}
