using Application.Repositories;
using Ecommerce.Domain.Repositories;
using Ecommerce.Presenters;
using Ecommerce.Services;
using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce;

public class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        RegisterServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var programManager = serviceProvider.GetService<ProgramManager>();

        programManager.Run();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        // Registering Services
        services.AddScoped<AuthenticationService>();
        services.AddScoped<CartService>();
        services.AddScoped<ProductService>();
        services.AddScoped<OrderService>();

        // Registering Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IAuthRepository, UserRepository>();
        services.AddScoped<ICartRepository, CartRepository>();

        // Registering Presenters
        services.AddScoped<AuthPresenter>();
        services.AddScoped<UserPresenter>();
        services.AddScoped<AdminPresenter>();
        services.AddScoped<CartPresenter>();
        services.AddScoped<OrderPresenter>();
        services.AddScoped<ProductPresenter>();
        services.AddScoped<StockPresenter>();
        services.AddScoped<MainPresenter>();

        // Registering Program Manager
        services.AddScoped<ProgramManager>();
        services.AddScoped<Database>();
    }
}