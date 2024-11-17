using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface IAuthRepository : IGenericRepository<User>
{
    User? FindByUsername(string username);

    User? FindByEmail(string email);
}
