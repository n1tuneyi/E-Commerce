using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.newRepositories;

public class UserRepository : RepositoryBase<User>, IAuthRepository
{
    public UserRepository(AppDbContext context) : base(context)
    { }

    public User? FindById(long userId, bool trackChanges)
    {
        return FindByCondition(u => u.Id == userId, trackChanges).SingleOrDefault();
    }

    public User? FindByUsername(string username, bool trackChanges)
    {
        return FindByCondition(u => u.Username == username, trackChanges).SingleOrDefault();
    }

    public User? FindByEmail(string email, bool trackChanges)
    {
        return FindByCondition(u => u.Email == email, trackChanges).SingleOrDefault();
    }

    public IEnumerable<User> GetAll(bool trackChanges)
    {
        return FindAll(trackChanges).ToList();
    }
}
