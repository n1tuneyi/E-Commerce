namespace Domain.Errors;

public sealed class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(long prodId)
    : base($"The product with id: {prodId} is not found")
    {
    }
}
