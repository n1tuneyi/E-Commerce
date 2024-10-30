using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface ICartRepository : IGenericRepository<ShoppingCart>
{
    ShoppingCart? FindByUserId(long userID);
    void RemoveItem(long prodID, long userID);
}
