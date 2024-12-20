﻿using Application.Interfaces;
using Application.Repositories;
using Ecommerce;
using Ecommerce.Presenters;
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

        services.AddSingleton<ILoggerService, LoggerService>();

        return services;
    }
    public static IServiceCollection ConfigureDB(this IServiceCollection services, IConfiguration config)
    {
        var connString = config.GetConnectionString("DefaultConnection");
        return services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connString));
    }

}
