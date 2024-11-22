using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.newRepositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public IEnumerable<Order> GetOrders(long userId, bool trackChanges)
    {
        return FindByCondition(o => o.UserId == userId, trackChanges)
            .Include(o => o.Items)
            .ThenInclude(oi => oi.Product)
            .ToList();
    }

}
