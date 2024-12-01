using Ecommerce.Domain.Entities;
using Infrastructure.EntitiesConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<CartItem> Items { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ShoppingCart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }


    public AppDbContext()
    { }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ECommerce_App;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());


        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.IsDeleted);


        modelBuilder.Entity<CartItem>()
            .HasQueryFilter(c => !c.IsDeleted);


        modelBuilder.Entity<OrderItem>()
            .HasQueryFilter(o => !o.IsDeleted);


        modelBuilder.Entity<Order>()
            .HasQueryFilter(o => !o.IsDeleted);

    }
}
