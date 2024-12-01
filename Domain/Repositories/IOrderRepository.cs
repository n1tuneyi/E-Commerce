using Domain.Repositories;
using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface IOrderRepository : IRepositoryBase<Order>
{
    Task<IEnumerable<Order>> GetOrdersAsync(string userId, bool trackChanges);
}
