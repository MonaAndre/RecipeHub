using RecipeHub.DTOs.ProductDTOs;
using RecipeHub.Models;
using RecipeHub.Repositories.Interfaces;

namespace RecipeHub.Endpoints;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products")
            .WithTags("Products");

        group.MapPost("/", async (
            ProductDtoRequest dto,
            IProductRepository repo) =>
        {
            var response = await repo.CreateProductAsync(dto);
            return Results.Created($"/products/{response.Id}", response);
        });

        group.MapGet("/", async (Domain.RecipeHub recipeHub) =>
        {
            var products = await recipeHub.GetAllProductsAsync();
            return Results.Ok(products);
        });

        group.MapPut("/{id:int}", async (
            int id,
            ProductDtoRequest dto,
            IProductRepository repo) =>
        {
            var updatedProduct = await repo.UpdateProductAsync(id, dto);
            return Results.Ok(updatedProduct);
        });

        group.MapDelete("/{id:int}", async (
            int id,
            IProductRepository repo) =>
        {
            var deleted = await repo.DeleteProductAsync(id);
            return deleted
                ? Results.Ok(new { message = "Product deleted successfully" })
                : Results.NotFound(new { message = "Product not found" });
        });

        return group;
    }
}