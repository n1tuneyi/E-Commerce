﻿namespace Domain.Request;

public abstract class RequestParameters
{
    public int PageNumber { get; set; } = 1;

    const int maxPageSize = 50;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;

        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }

    public string? OrderBy { get; set; }
}
