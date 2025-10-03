using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductoService, ProductoService>();
        services.AddScoped<ICategoriaService, CategoriaService>();

        return services;
    }
}
