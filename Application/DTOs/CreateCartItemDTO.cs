namespace Application.DTOs;

public record CreateCartItemDTO(long ProductId, int Quantity);

public record ViewCartItemDTO(long ProductId, string Name, string Description,
                int Quantity, decimal Price, decimal TotalPrice, int StockQuantity);
