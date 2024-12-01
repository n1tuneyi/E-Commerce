using Domain.Repositories;
using Domain.Request.Product;
using Domain.RequestFeatures;
using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<Product> GetProductAsync(Guid prodId, bool trackChanges);
    Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters, bool trackChanges);
}