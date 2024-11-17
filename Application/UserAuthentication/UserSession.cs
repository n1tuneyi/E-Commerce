using Ecommerce.Domain.Entities;

namespace Presentation.Authentication;

public class UserSession
{
    public static User CurrentUser { get; set; }
}
