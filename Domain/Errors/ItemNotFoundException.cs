namespace Domain.Errors;

public class ItemNotFoundException : NotFoundException
{
    public ItemNotFoundException(Guid prodId) : base($"Item {prodId} is not found in the cart!")
    {

    }
}
