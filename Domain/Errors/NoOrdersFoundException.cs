namespace Domain.Errors;

public class NoOrdersFoundException : NotFoundException
{
    public NoOrdersFoundException() : base("No Orders yet.")
    { }
}
