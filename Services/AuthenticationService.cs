using Ecommerce.Domain;
using Ecommerce.Repositories;

namespace Ecommerce.Services;

public static class AuthenticationService
{
    public static User? Signup(User user)
    {
        return UserRepository.Create(user);
    }

    public static User Login(string username, string password)
    {
        User? user = UserRepository.FindByUsername(username);

        if (user?.Password == password)
            return user;

        else
            throw new ArgumentException("Username or Password is incorrect");
    }

}
