using Application.Interfaces;
using Application.Repositories;
using Ecommerce.Domain.Entities;
using Presentation.Authentication;

namespace Ecommerce.Services;

public class ProductService
{
    private readonly IProductRepository _repository;
    private readonly ILoggerService _loggerService;

    public ProductService(IProductRepository repository, ILoggerService loggerService)
    {
        _repository = repository;
        _loggerService = loggerService;
    }

    public Product? FindById(long prodID)
    {
        return _repository.GetProduct(prodID, trackChanges: true);
    }
    public IEnumerable<Product> GetProducts()
    {
        return _repository.GetAllProducts(trackChanges: false);
    }

    public void Add(Product product)
    {
        _loggerService.LogInformation($"Product#{product.Id} " +
            $"just got added to stock with {product.StockQuantity} quantity and price:{product.Price:C}");

        _repository.Create(product);
    }

    public bool Exists(long prodID)
    {
        return FindById(prodID) is not null;
    }

    public void Remove(long prodID)
    {
        _loggerService.LogInformation($"Product#{prodID} is removed by Admin#{UserSession.CurrentUser.Id}");
        Product? RemovedProduct = FindById(prodID);
        _repository.Delete(RemovedProduct);
    }


    // Product Consumed by a customer
    public void ConsumeProductStock(long productId, int requestedQuantity)
    {
        Product product = FindById(productId);

        product.StockQuantity -= requestedQuantity;

        _loggerService.LogInformation($"Product#{productId} stock decreased by {requestedQuantity} amount");

        _repository.Update(product);
    }

    public void UpdateName(long productId, string newName)
    {
        Product product = FindById(productId);
        product.Name = newName;
        product.UpdatedDate = DateTime.Now;
        _repository.Update(product);
    }

    public void UpdateDescription(long productId, string newDescription)
    {
        Product product = FindById(productId);
        product.Description = newDescription;
        product.UpdatedDate = DateTime.Now;
        _repository.Update(product);
    }
    public void UpdatePrice(long productId, decimal newPrice)
    {
        Product product = FindById(productId);
        product.Price = newPrice;
        product.UpdatedDate = DateTime.Now;
        _repository.Update(product);
    }
    // Product Stock changed by an Admin
    public void UpdateStockQuantity(long productId, int newQuantity)
    {
        Product product = FindById(productId);
        product.StockQuantity = newQuantity;
        product.UpdatedDate = DateTime.Now;
        _repository.Update(product);
    }
}
