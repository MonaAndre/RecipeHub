using RecipeHub.Common;
using RecipeHub.Domain;
using RecipeHub.DTOs.ProductDTOs;

namespace RecipeHub.Endpoints;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products")
            .WithTags("Products");

        group.MapGet("/", async (IRecipeHub recipeHub) =>
        {
            var result = await recipeHub.GetAllProductsAsync();
            return result.ToHttpResult();
        });

        group.MapPost("/", async (
            ProductDtoRequest dto,
            IRecipeHub recipeHub) =>
        {
            var result = await recipeHub.CreateProductAsync(dto);
            return result.ToHttpResult();
        }).AddEndpointFilter(async (context, next) =>
        {
            var dto = context.Arguments.OfType<ProductDtoRequest>().FirstOrDefault();

            if (dto is null)
                return Results.BadRequest("Invalid request body");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return Results.BadRequest("ProductName is required");

            if (dto.Name.Length > 100)
                return Results.BadRequest("ProductName is too long");

            return await next(context);
        });

        group.MapPut("/{id:int}", async (
            int id,
            ProductDtoRequest dto,
            IRecipeHub recipeHub) =>
        {
            var result = await recipeHub.UpdateProductAsync(id, dto);
            return result.ToHttpResult();
        });

        group.MapDelete("/{id:int}", async (
            int id,
            IRecipeHub recipeHub) =>
        {
            var result = await recipeHub.DeleteProductAsync(id);
            return result.ToHttpResult();
        });

        return group;
    }
}