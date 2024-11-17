using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<CartItem> Items { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ShoppingCart> Carts { get; set; }
    public DbSet<User> Users { get; set; }
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
        modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Name = "Apples",
                    Description = "American Apples with rich Vitamins and good taste",
                    Price = 8,
                    StockQuantity = 60
                },
                new Product()
                {
                    Id = 2,
                    Name = "Oranges",
                    Description = "Egyptian Oranges",
                    Price = 4,
                    StockQuantity = 100
                }
            );


        modelBuilder.Entity<User>().HasData(
             new User()
             {
                 Id = 1,
                 Username = "youssefhm",
                 Email = "youssefhammam77@gmail.com",
                 Password = "1234",
                 Address = "5 St Ismail Basha - El Sayeda Zeinab"
             },
             new User()
             {
                 Id = 2,
                 Username = "admin",
                 Email = "admin@admin.com",
                 Password = "1234",
                 Address = "5st Alexandria - Egypt"
             }
       );

        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.IsDeleted);


        modelBuilder.Entity<CartItem>()
            .HasQueryFilter(c => !c.IsDeleted);


        modelBuilder.Entity<User>()
            .HasQueryFilter(u => !u.IsDeleted);


        modelBuilder.Entity<OrderItem>()
            .HasQueryFilter(o => !o.IsDeleted);


        modelBuilder.Entity<Order>()
            .HasQueryFilter(o => !o.IsDeleted);
    }
}
