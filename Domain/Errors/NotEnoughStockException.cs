namespace Domain.Errors;

public class NotEnoughStockException : BadRequestException
{
    public NotEnoughStockException() : base("Not Enough Stock!")
    {

    }
}
