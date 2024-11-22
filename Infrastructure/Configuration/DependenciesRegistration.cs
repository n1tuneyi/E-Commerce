using Application.Interfaces;
using Application.Repositories;
using Ecommerce.Services;
using Infrastructure.Context;
using Infrastructure.Logging;
using Infrastructure.newRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration;

public static class DependenciesRegistration
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
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

        services.AddSingleton<ILoggerService, LoggerService>();

        return services;
    }
    public static IServiceCollection ConfigureDB(this IServiceCollection services, IConfiguration config)
    {
        var connString = config.GetConnectionString("DefaultConnection");
        return services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connString));
    }
    public static IServiceCollection ConfigureLogging(this IServiceCollection services)
    {
        return services.AddSingleton<ILoggerService, LoggerService>();
    }

}
