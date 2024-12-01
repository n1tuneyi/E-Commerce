﻿namespace Domain.Errors;

public class InvalidRangeBadRequestException : BadRequestException
{
    public InvalidRangeBadRequestException() : base("Invalid Price Range!")
    {
    }
}
