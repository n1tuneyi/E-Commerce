namespace Application.DTOs.Cart;

public record CartDTO
{
    public List<ViewCartItemDTO> Items { get; set; }
    public decimal TotalPrice { get; set; }
}