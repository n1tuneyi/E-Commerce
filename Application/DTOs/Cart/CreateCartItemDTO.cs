namespace Application.DTOs.Cart;

public record CreateCartItemDTO(Guid ProductId, int Quantity);

public record ViewCartItemDTO(Guid ProductId, string ProductName, string ProductDescription,
                int Quantity, decimal ProductPrice, decimal TotalPrice, int ProductStockQuantity);
public record CartItemUpdateDTO(int quantity);