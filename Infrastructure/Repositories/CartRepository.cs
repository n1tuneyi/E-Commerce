using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.newRepositories;

public class CartRepository : RepositoryBase<ShoppingCart>, ICartRepository
{
    public CartRepository(AppDbContext context) : base(context)
    { }

    public async Task<ShoppingCart> GetCartAsync(string userId, bool trackChanges)
    {
        return await FindByCondition(c => c.UserId == userId, trackChanges)
                             .Include(c => c.Items)
                             .ThenInclude(i => i.Product)
                             .SingleAsync();
    }

    public async Task RemoveItemAsync(CartItem item, ShoppingCart userCart)
    {
        bool softDelete = true;

        if (softDelete)
            item.IsDeleted = true;

        else
            _context.Items.Remove(item);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(string userId)
    {
        return (await GetCartAsync(userId, false)) is not null;
    }

}
