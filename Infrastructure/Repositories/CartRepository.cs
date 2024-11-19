using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.newRepositories;

public class CartRepository : RepositoryBase<ShoppingCart>, ICartRepository
{
    public CartRepository(AppDbContext context) : base(context)
    { }

    public ShoppingCart? GetByUserId(long userId, bool trackChanges)
    {
        return FindByCondition(c => c.UserId == userId, trackChanges)
                             .Include(c => c.Items)
                             .ThenInclude(i => i.Product)
                             .SingleOrDefault();
    }

    public void RemoveItem(CartItem item, ShoppingCart userCart)
    {
        bool softDelete = true;

        if (softDelete)
            item.IsDeleted = true;

        else
            _context.Items.Remove(item);

    }

    public bool Exists(long userId)
    {
        return GetByUserId(userId, false) is not null;
    }

}
