using Application.Repositories;
using Ecommerce.Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.newRepositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public Product? GetProduct(long prodId, bool trackChanges)
    {
        return FindByCondition(p => p.Id == prodId, trackChanges)
              .SingleOrDefault();
    }

    public IEnumerable<Product> GetAllProducts(bool trackChanges)
    {
        return FindAll(trackChanges).ToList();
    }
}

