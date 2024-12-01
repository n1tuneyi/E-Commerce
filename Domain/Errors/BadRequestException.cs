namespace Domain.Errors;

public abstract class BadRequestException : Exception
{
    protected BadRequestException(string message) : base(message) { }
}
