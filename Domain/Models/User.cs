﻿using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain;

public class User
{
    public long Id { get; set; }

    public string Username { get; set; } = "";

    public string Password { get; set; } = "";

    [EmailAddress]
    public string Email { get; set; } = "";

    public string Address { get; set; } = "";
}

