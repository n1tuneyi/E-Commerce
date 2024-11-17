using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Database;

namespace Ecommerce.Domain.Repositories;

public class UserRepository : IAuthRepository
{
    private readonly Database _db;
    private readonly AppDbContext _context;

    public UserRepository(Database db, AppDbContext context)
    {
        _db = db;
        _context = context;
    }

    public User Create(User user)
    {
        _context.Users.Add(user);

        _context.SaveChanges();

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
        return _context.Users.FirstOrDefault(user => user.Id == id);
    }

    public User? FindByUsername(string username)
    {
        return _context.Users.FirstOrDefault(user => user.Username == username);
    }

    public User? FindByEmail(string email)
    {
        return _context.Users.FirstOrDefault(user => user.Email == email);
    }

    public List<User> GetAll()
    {
        return _context.Users.ToList();
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
