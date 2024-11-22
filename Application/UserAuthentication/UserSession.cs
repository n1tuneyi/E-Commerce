using Ecommerce.Domain.Entities;

namespace Application.Authentication;

public class UserSession
{
    public static User CurrentUser { get; set; }
}
