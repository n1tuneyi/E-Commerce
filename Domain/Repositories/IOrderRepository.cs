using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    List<Order> GetOrdersByUserId(long userID);
}
