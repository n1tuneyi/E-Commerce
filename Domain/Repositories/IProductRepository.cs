using Ecommerce.Domain.Entities;

namespace Application.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    void RemoveFromStock(long id, int quantity);
}