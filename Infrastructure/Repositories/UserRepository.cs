using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Database;

namespace Ecommerce.Domain.Repositories;

public class UserRepository : IAuthRepository
{
    private readonly Database _db;

    public UserRepository(Database db)
    {
        _db = db;
    }

    public static User? currentUser { get; set; }

    public User Create(User user)
    {
        bool isExistingUsername = FindByUsername(user.Username) is not null;

        bool isExistingEmail = FindByEmail(user.Email) is not null;

        if (isExistingUsername)
            throw new ArgumentException("username already exists!");

        if (isExistingEmail)
            throw new ArgumentException("email already exists!");

        user.Id = _db.Users.Count + 1;

        _db.Users.Add(user);

        return user;
    }

    public User? Remove(long id)
    {
        User? user = _db.Users.Find(user => user.Id == id);

        if (user is null) return null;

        _db.Users.Remove(user);

        return user;
    }

    public User? FindById(long id)
    {
        return _db.Users.FirstOrDefault(user => user.Id == id);
    }

    public User? FindByUsername(string username)
    {
        return _db.Users.FirstOrDefault(user => user.Username == username);
    }

    public User? FindByEmail(string email)
    {
        return _db.Users.FirstOrDefault(user => user.Email == email);
    }

    public List<User> GetAll()
    {
        return _db.Users;
    }

    public void Update(User updatedEntity)
    {
        throw new NotImplementedException();
    }

    void IGenericRepository<User>.Remove(long entityId)
    {
        throw new NotImplementedException();
    }

    public void Remove(User entity)
    {
        throw new NotImplementedException();
    }

    public bool Exists(long entityID)
    {
        throw new NotImplementedException();
    }
}
