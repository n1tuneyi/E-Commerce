using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(SeedData());
    }
    public Product[] SeedData()
    {
        return [
                   new Product()
                   {
                       Id = Guid.NewGuid(),
                       Name = "Apples",
                       Description = "American Apples with rich Vitamins and good taste",
                       Price = 8,
                       StockQuantity = 60
                   },
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Oranges",
                Description = "Egyptian Oranges",
                Price = 4,
                StockQuantity = 100
            }

            ];
    }

}
