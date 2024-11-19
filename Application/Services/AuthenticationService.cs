using Application.Interfaces;
using Application.Repositories;
using Ecommerce.Domain.Entities;
using Presentation.Authentication;

namespace Ecommerce.Services;

public class AuthenticationService
{
    private readonly IAuthRepository _repository;
    private readonly ILoggerService _loggerService;

    public AuthenticationService(IAuthRepository repository, ILoggerService loggerService)
    {
        _repository = repository;
        _loggerService = loggerService;
    }

    public User Signup(User user)
    {
        bool isExistingUsername = _repository.FindByUsername(user.Username, trackChanges: false) is not null;

        bool isExistingEmail = _repository.FindByEmail(user.Email, trackChanges: false) is not null;

        if (isExistingUsername)
            throw new ArgumentException("username already exists!");

        if (isExistingEmail)
            throw new ArgumentException("email already exists!");

        _loggerService.LogInformation($"{user.Username} just signed up");

        return _repository.Create(user);
    }

    public User Login(string username, string password)
    {
        User? user = _repository.FindByUsername(username, trackChanges: false);

        if (user?.Password == password)
        {
            _loggerService.LogInformation($"User {username} has logged in");
            return user;
        }

        else
            throw new ArgumentException("Username or Password is incorrect");
    }

    public void Logout()
    {
        // Logs out current user
        _loggerService.LogInformation($"User {UserSession.CurrentUser.Username} just logged out!");

        UserSession.CurrentUser = null;
    }

}
