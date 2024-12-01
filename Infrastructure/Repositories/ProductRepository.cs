using Application.Repositories;
using Domain.Request.Product;
using Domain.RequestFeatures;
using Ecommerce.Domain.Entities;
using Infrastructure.Context;
using Infrastructure.RepositoriesExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.newRepositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<Product> GetProductAsync(Guid prodId, bool trackChanges)
    {
        return await FindByCondition(p => p.Id == prodId, trackChanges)
              .SingleAsync();
    }

    public async Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters, bool trackChanges)
    {
        var products = await FindAll(trackChanges)
                    .FilterProducts(productParameters.MinPrice, productParameters.MaxPrice)
                    .Search(productParameters.SearchTerm)
                    .Sort(productParameters.OrderBy)
                    .ToListAsync();
        //.Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
        //.Take(productParameters.PageSize)

        var count = await FindAll(trackChanges).CountAsync();

        return PagedList<Product>.ToPagedList(products, count, productParameters.PageNumber, productParameters.PageSize);
    }
}

