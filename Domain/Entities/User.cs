using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Entities;

public class User : BaseEntity<long>
{
    public long Id { get; set; }

    public string Username { get; set; } = "";

    public string Password { get; set; } = "";

    [EmailAddress]
    public string Email { get; set; } = "";
    public string Address { get; set; } = "";

    public ShoppingCart Cart { get; set; }

    public ICollection<Order> Orders { get; set; }
}

