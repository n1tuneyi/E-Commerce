using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.newRepositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public IEnumerable<Order> GetOrders(long userId, bool trackChanges)
    {
        return FindByCondition(o => o.UserId == userId, trackChanges).ToList();
    }

}
