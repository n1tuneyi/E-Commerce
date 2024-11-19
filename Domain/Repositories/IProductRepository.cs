using Domain.Repositories;
using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface IProductRepository : IRepositoryBase<Product>
{
    Product? GetProduct(long prodId, bool trackChanges);
    IEnumerable<Product> GetAllProducts(bool trackChanges);
}