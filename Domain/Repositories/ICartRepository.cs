using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface ICartRepository : IGenericRepository<ShoppingCart>
{
    ShoppingCart? FindByUserId(long userID);
    void RemoveItem(CartItem removedItem, ShoppingCart userCart);
}
