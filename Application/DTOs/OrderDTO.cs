namespace Application.DTOs;

public record OrderDTO
{
    public long Id { get; init; }
    public DateTime OrderDate { get; init; }
    public decimal TotalAmount { get; init; }
    public List<OrderItemDTO> Items { get; init; }
}

public record OrderItemDTO
{
    public long ProductId { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}
