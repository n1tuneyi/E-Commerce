namespace Ecommerce.Domain.Entities;

public class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime Date { get; set; }

    public List<CartItem> Items { get; set; }

    public decimal TotalAmount { get; set; }
}
