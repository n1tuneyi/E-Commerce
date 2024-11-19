using Domain.Repositories;
using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface IOrderRepository : IRepositoryBase<Order>
{
    IEnumerable<Order> GetOrders(long userId, bool trackChanges);
}
