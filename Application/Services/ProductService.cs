using Application.Repositories;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public List<Product> GetProducts()
    {
        return _repository.GetAll();
    }

    public Product Add(Product product)
    {
        return _repository.Create(product);
    }

    public bool Exists(long id)
    {
        return _repository.FindById(id) is not null;
    }

    public void Remove(long productId)
    {
        _repository.RemoveFromStock(productId, int.MaxValue);
    }

    public Product? Get(long productId)
    {
        return _repository.FindById(productId);
    }

    public void RemoveProduct(long productId)
    {
        _repository.Remove(productId);
    }

    public void UpdateName(long productId, string newName)
    {
        Product product = _repository.FindById(productId);
        product.Name = newName;
    }
    public void UpdateDescription(long productId, string newDescription)
    {
        Product product = _repository.FindById(productId);
        product.Description = newDescription;
    }
    public void UpdatePrice(long productId, decimal newPrice)
    {
        Product product = _repository.FindById(productId);
        product.Price = newPrice;
    }

    public void UpdateQuantity(long productId, int newQuantity)
    {
        Product product = _repository.FindById(productId);
        product.StockQuantity = newQuantity;
    }
}
