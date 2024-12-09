using Application.DTOs.Product;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Domain.Errors;
using Domain.Request.Product;
using Domain.RequestFeatures;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Services;

public class ProductService
{
    private readonly IProductRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, ILoggerService loggerService, IMapper mapper)
    {
        _repository = repository;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Product> GetProductAsync(Guid prodID)
    {
        var prod = await _repository.GetProductAsync(prodID, trackChanges: true);

        if (prod is null)
            throw new ProductNotFoundException(prodID);

        return prod;
    }

    public async Task<(IEnumerable<Product> products, MetaData metaData)> GetProductsAsync(ProductParameters productParameters)
    {
        var productsWithMetaData = await _repository.GetAllProductsAsync(productParameters, trackChanges: false);

        if (!productParameters.ValidPriceRange)
            throw new InvalidRangeBadRequestException();

        var products = _mapper.Map<IEnumerable<Product>>(productsWithMetaData);

        return (products, metaData: productsWithMetaData.MetaData);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _loggerService.LogInformation($"Product#{product.Id} " +
            $"just got added to stock with {product.StockQuantity} quantity and price:{product.Price:C}");

        return await _repository.CreateAsync(product);
    }

    public bool Exists(Guid prodID)
    {
        return GetProductAsync(prodID) is not null;
    }

    public async Task<Product> RemoveAsync(Guid prodID)
    {
        //_loggerService.LogInformation($"Product#{prodID} is removed by Admin#{UserSession.CurrentUser.Id}");
        Product? RemovedProduct = await GetProductAsync(prodID);
        return await _repository.DeleteAsync(RemovedProduct);
    }

    public void DeductStock(List<OrderItem> items)
    {
        foreach (var item in items)
        {
            if (item.Product.StockQuantity < item.Quantity)
                throw new NotEnoughStockException();

            item.Product.StockQuantity -= item.Quantity;
        }
    }

    public async Task UpdateProductAsync(Guid prodId, ProductUpdateDto productToUpdate)
    {
        Product product = await GetProductAsync(prodId);

        Console.WriteLine($"price: {productToUpdate.Price}, stockQuantity: {productToUpdate.StockQuantity}");

        _mapper.Map(productToUpdate, product);

        await _repository.UpdateAsync(product);
    }
}
