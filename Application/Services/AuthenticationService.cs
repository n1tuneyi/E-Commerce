using Application.Repositories;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Services;

public class AuthenticationService
{
    private readonly IAuthRepository _repository;

    public AuthenticationService(IAuthRepository repository)
    {
        _repository = repository;
    }

    public User Signup(User user)
    {
        return _repository.Create(user);
    }

    public User Login(string username, string password)
    {
        User? user = _repository.FindByUsername(username);

        if (user?.Password == password)
            return user;

        else
            throw new ArgumentException("Username or Password is incorrect");
    }

}
