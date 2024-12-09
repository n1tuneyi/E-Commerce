namespace Domain.Errors
{
    public class NoCartItemsFoundException : NotFoundException
    {
        public NoCartItemsFoundException() : base("Cart has no items")
        { }
    }
}
