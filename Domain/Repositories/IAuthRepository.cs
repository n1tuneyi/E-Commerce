using Domain.Repositories;
using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface IAuthRepository : IRepositoryBase<User>
{
    User? FindByUsername(string username, bool trackChanges);

    User? FindByEmail(string email, bool trackChanges);
}
