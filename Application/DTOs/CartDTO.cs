namespace Application.DTOs;

public record CartDTO
{
    public List<ViewCartItemDTO> Items { get; set; }
    public decimal TotalPrice { get; set; }
}