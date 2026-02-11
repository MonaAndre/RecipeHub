using Microsoft.EntityFrameworkCore;
using RecipeHub.Data;
using RecipeHub.Repositories.Implementations;
using RecipeHub.Repositories.Interfaces;

namespace RecipeHub.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddOpenApi();
        return services;
    }

}