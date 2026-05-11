using AHuiHou.Application.Interfaces;
using AHuiHou.Application.Services;
using AHuiHou.Application.Validators;
using AHuiHou.Domain.Interfaces;
using AHuiHou.Infrastructure.Data;
using AHuiHou.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace AHuiHou.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Validators
        services.AddValidatorsFromAssemblyContaining<RegisterUserRequestValidator>();
        services.AddFluentValidationAutoValidation();

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ILoyaltyService, LoyaltyService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITableService, TableService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<AHuiHouDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Security
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}

