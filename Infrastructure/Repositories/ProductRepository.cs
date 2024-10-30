using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Database;

namespace Ecommerce.Domain.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly Database _db;

    public ProductRepository(Database db)
    {
        _db = db;
    }

    public Product Create(Product product)
    {
        product.Id = _db.Products.Count + 1;

        _db.Products.Add(product);

        return product;
    }

    public bool Exists(long entityID)
    {
        throw new NotImplementedException();
    }

    public Product? FindById(long id)
    {
        return _db.Products.Find(product => product.Id == id);
    }

    public List<Product> GetAll()
    {
        return _db.Products;
    }

    public void Remove(long id)
    {
        Product? removedProduct = _db.Products.Find(product => product.Id == id);

        _db.Products.Remove(removedProduct);
    }

    public void Remove(Product entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveFromStock(long id, int quantity)
    {
        Product product = _db.Products.Find(product => product.Id == id)!;

        if (product.StockQuantity < quantity)
            throw new ArgumentException($"No enough stock only {product.StockQuantity} left");

        product.StockQuantity -= quantity;
    }

    public void Update(Product newProduct)
    {
        Product product = _db.Products.Find(product => product.Id == newProduct.Id);
    }
}
