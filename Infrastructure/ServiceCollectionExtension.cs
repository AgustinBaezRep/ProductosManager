using Application.Abstraction;
using Application.Abstraction.ExternalServices;
using Domain.Entities;
using Infrastructure.ExternalServices;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        return services
            .AddDatabaseConfiguration(builder)
            .AddAuthenticationConfiguration(builder)
            .AddAuthorizationConfiguration()
            .AddRepositories()
            .AddExternalServices();
    }

    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        return services.AddDbContext<ProductosManagerDbContext>(
            options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
    }

    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

        return services;
    }

    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(nameof(TipoRol.Administrador), policy => policy.RequireRole(nameof(TipoRol.Administrador)));
            options.AddPolicy(nameof(TipoRol.Cliente), policy => policy.RequireRole(nameof(TipoRol.Cliente)));
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductoRepository, ProductoRespository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }

    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
