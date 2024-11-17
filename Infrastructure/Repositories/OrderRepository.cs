using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Database;

namespace Ecommerce.Domain.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly Database _db;
    private readonly AppDbContext _context;

    public OrderRepository(Database db, AppDbContext context)
    {
        _db = db;
        _context = context;
    }

    public List<Order> GetOrdersByUserId(long userId)
    {
        return _context.Orders
                       .Where(order => order.UserId == userId)
                       .ToList();
    }

    public Order Create(Order order)
    {
        _context.Orders.Add(order);

        _context.SaveChanges();

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
