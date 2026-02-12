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
            var product = new Product
            {
                ProductNamn = dto.Name,
                Category = dto.Category
            };

            await repo.CreateProductAsync(product);

            var response = new ProductDtoResponse
            {
                Name = product.ProductNamn,
                Category = product.Category,
                Id = product.ProductId
            };

            return Results.Created($"/products/{response.Id}", response);
        });

        group.MapGet("/", async (IProductRepository repo) =>
        {
            var products = await repo.GetAllProductsAsync();

            var response = products.Select(p => new ProductDtoResponse
            {
                Id = p.ProductId,
                Name = p.ProductNamn,
                Category = p.Category!
            });

            return Results.Ok(response);
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