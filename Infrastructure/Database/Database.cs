using Ecommerce.Domain.Entities;

namespace Infrastructure.Database;

public class Database
{

    public Database() { }

    private static readonly List<ShoppingCart> carts = new List<ShoppingCart>();

    private static readonly List<Order> orders = new List<Order>();

    private static readonly List<Product> products = [
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
    ];

    private static readonly List<User> users =
    [
       new User()
       {
           Username = "youssefhm",
           Email = "youssefhammam77@gmail.com",
           Password = "1234",
           Address = "5 St Ismail Basha - El Sayeda Zeinab"
       },
        new User()
        {
            Username = "admin",
            Email = "admin@admin.com",
            Password = "1234",
            Address = "5st Alexandria - Egypt"
        }
    ];

    public List<ShoppingCart> Carts => carts;
    public List<User> Users => users;
    public List<Product> Products => products;
    public List<Order> Orders => orders;

}
