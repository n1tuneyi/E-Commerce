using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<string>? Roles { get; init; }
}

