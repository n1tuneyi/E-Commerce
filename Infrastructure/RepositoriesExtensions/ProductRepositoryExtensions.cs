using Ecommerce.Domain.Entities;
using Infrastructure.RepositoriesExtensions.Utility;
using System.Linq.Dynamic.Core;

namespace Infrastructure.RepositoriesExtensions;

public static class ProductRepositoryExtensions
{
    public static IQueryable<Product> FilterProducts(this IQueryable<Product> products, decimal minPrice, decimal maxPrice)
    {
        return products.Where(p => p.Price <= maxPrice && p.Price >= minPrice);
    }

    public static IQueryable<Product> Search(this IQueryable<Product> products, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return products;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return products.Where(p => p.Name.Trim().ToLower().Contains(lowerCaseTerm));
    }



    public static IQueryable<Product> Sort(this IQueryable<Product> products, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return products.OrderBy(p => p.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Product>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return products.OrderBy(p => p.Name);

        return products.OrderBy(orderQuery);
    }
}
