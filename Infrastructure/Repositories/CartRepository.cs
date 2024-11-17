using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Domain.Repositories;

public class CartRepository : ICartRepository
{
    private readonly Database _db;
    private readonly AppDbContext _context;

    public CartRepository(Database db, AppDbContext context)
    {
        _db = db;
        _context = context;
    }

    public ShoppingCart Create(ShoppingCart cart)
    {
        _context.Carts.Add(cart);

        _context.SaveChanges();

        return cart;
    }

    public ShoppingCart? FindByUserId(long id)
    {
        string dbugMsg = _context.Carts.Include(c => c.Items)
                             .ThenInclude(i => i.Product).ToQueryString();
        Console.WriteLine(dbugMsg);
        return _context.Carts.Include(c => c.Items)
                             .ThenInclude(i => i.Product)
                             .FirstOrDefault(cart => cart.UserId == id);
    }

    public void RemoveItem(CartItem removedItem, ShoppingCart userCart)
    {
        bool softDelete = true;

        if (softDelete)
        {
            removedItem.IsDeleted = true;
        }

        else
        {
            _context.Items.Remove(removedItem);
        }

        _context.SaveChanges();
        userCart.Items.Remove(removedItem);
    }

    public void Remove(ShoppingCart cart)
    {
        if (cart is null)
            return;

        _context.SaveChanges();
    }

    public void Update(ShoppingCart updatedEntity)
    {
        _context.Update(updatedEntity);
        _context.SaveChanges();
    }
    public ShoppingCart? FindById(long entityID)
    {
        return _context.Carts.Include(c => c.Items).FirstOrDefault(c => c.Id == entityID);
    }
    public bool Exists(long id)
    {
        return FindByUserId(id) is not null;
    }

    public List<ShoppingCart> GetAll()
    {
        throw new NotImplementedException();
    }
    public void Remove(long entityId)
    {
        throw new NotImplementedException();
    }
}
