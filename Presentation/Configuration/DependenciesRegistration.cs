using Application.Interfaces;
using Application.Repositories;
using Ecommerce;
using Ecommerce.Domain.Repositories;
using Ecommerce.Presenters;
using Ecommerce.Services;
using Infrastructure.Data;
using Infrastructure.Database;
using Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Configuration;

public static class DependenciesRegistration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
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

        //services.AddScoped<AppDbContext>();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Data Source=.;Initial Catalog=ECommerce_App;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")
        );
        // Registering Services
        services.AddScoped<ProductService>();

        services.AddSingleton<ILoggerService, LoggerService>();

        return services;
    }

}
