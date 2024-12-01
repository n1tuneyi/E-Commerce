namespace Application.DTOs.Order;

public record OrderDTO
{
    public Guid Id { get; init; }
    public DateTime Date { get; init; }
    public decimal TotalAmount { get; init; }
    public List<OrderItemDTO> Items { get; init; }
}

