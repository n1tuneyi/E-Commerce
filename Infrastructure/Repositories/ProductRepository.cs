using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Database;

namespace Ecommerce.Domain.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly Database _db;
    private readonly AppDbContext _context;
    public ProductRepository(Database db, AppDbContext context)
    {
        _db = db;
        _context = context;
    }

    public Product Create(Product product)
    {
        _context.Products.Add(product);

        _context.SaveChanges();

        return product;
    }

    public Product? FindById(long id)
    {
        return _context.Products.FirstOrDefault(product => product.Id == id);
    }

    public List<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public void Update(Product newProduct)
    {
        _context.Products.Update(newProduct);

        _context.SaveChanges();
    }

    public void Remove(long id)
    {
        bool softDelete = true;

        Product? removedProduct = _context.Products.First(product => product.Id == id);

        if (softDelete)
        {
            removedProduct.IsDeleted = true;
        }
        else
        {
            _context.Products.Remove(removedProduct);
        }

        _context.SaveChanges();
    }

    public void Remove(Product entity)
    {
        throw new NotImplementedException();
    }
    public bool Exists(long entityID)
    {
        throw new NotImplementedException();
    }
}

