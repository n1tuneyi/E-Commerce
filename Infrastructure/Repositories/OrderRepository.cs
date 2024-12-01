using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.newRepositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Order>> GetOrdersAsync(string userId, bool trackChanges)
    {
        return await FindByCondition(o => o.UserId == userId, trackChanges)
            .Include(o => o.Items)
            .ThenInclude(oi => oi.Product)
            .ToListAsync();
    }

}
