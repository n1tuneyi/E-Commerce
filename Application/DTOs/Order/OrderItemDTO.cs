namespace Application.DTOs.Order;


public record OrderItemDTO
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public decimal TotalPrice { get; init; }
}


