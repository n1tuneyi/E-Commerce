using Domain.Repositories;
using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface ICartRepository : IRepositoryBase<ShoppingCart>
{
    Task<ShoppingCart> GetCartAsync(string userId, bool trackChanges);
    Task RemoveItemAsync(CartItem removedItem, ShoppingCart userCart);
    Task<bool> Exists(string userId);
}
