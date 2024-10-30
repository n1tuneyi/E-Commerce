using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Database;

namespace Ecommerce.Domain.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly Database _db;

    public OrderRepository(Database db)
    {
        _db = db;
    }

    public List<Order> GetOrdersByUserId(long userId)
    {
        return _db.Orders.FindAll(order => order.UserId == userId);
    }

    public Order Create(Order order)
    {
        order.Id = _db.Orders.Count + 1;

        _db.Orders.Add(order);

        return order;
    }

    public List<Order> GetAll()
    {
        throw new NotImplementedException();
    }

    public Order FindById(long entityID)
    {
        throw new NotImplementedException();
    }

    public void Update(Order updatedEntity)
    {
        throw new NotImplementedException();
    }

    public void Remove(long entityId)
    {
        throw new NotImplementedException();
    }

    public void Remove(Order entity)
    {
        throw new NotImplementedException();
    }

    public bool Exists(long entityID)
    {
        throw new NotImplementedException();
    }
}
