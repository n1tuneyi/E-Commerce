using Domain.Repositories;
using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface ICartRepository : IRepositoryBase<ShoppingCart>
{
    ShoppingCart? GetByUserId(long userId, bool trackChanges);
    void RemoveItem(CartItem removedItem, ShoppingCart userCart);
    bool Exists(long userId);
}
