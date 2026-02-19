using Microsoft.EntityFrameworkCore;
using RecipeHub.Data;
using RecipeHub.Domain;
using RecipeHub.Repositories.Implementations;
using RecipeHub.Repositories.Interfaces;
using RecipeHub.Services.Implemintations;
using RecipeHub.Services.Interfaces;

namespace RecipeHub.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IRecipeHub, Domain.RecipeHub>();
        services.AddOpenApi();
        return services;
    }
}