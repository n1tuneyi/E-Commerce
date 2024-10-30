using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Database;

namespace Ecommerce.Domain.Repositories;

public class CartRepository : ICartRepository
{
    private readonly Database _db;
    public CartRepository(Database db)
    {
        _db = db;
    }

    public ShoppingCart Create(ShoppingCart cart)
    {
        cart.Id = _db.Carts.Count + 1;

        _db.Carts.Add(cart);

        return cart;
    }

    public ShoppingCart? FindByUserId(long id)
    {
        return _db.Carts.FirstOrDefault(cart => cart.UserId == id);
    }

    public void RemoveItem(long prodID, long userID)
    {
        ShoppingCart userCart = _db.Carts.FirstOrDefault(cart => cart.UserId == userID)!;

        userCart.Items.RemoveAll(item => item.ProductId == prodID);
    }

    public void Remove(ShoppingCart cart)
    {
        _db.Carts.Remove(cart);
    }

    public bool Exists(long id)
    {
        return FindByUserId(id) is not null;
    }

    public List<ShoppingCart> GetAll()
    {
        throw new NotImplementedException();
    }

    public ShoppingCart FindById(long entityID)
    {
        throw new NotImplementedException();
    }

    public void Update(ShoppingCart updatedEntity)
    {
        throw new NotImplementedException();
    }

    public void Remove(long entityId)
    {
        throw new NotImplementedException();
    }
}
