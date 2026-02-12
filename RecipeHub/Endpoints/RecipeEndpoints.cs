using RecipeHub.DTOs.RecipeDTOs;
using RecipeHub.Repositories.Interfaces;

namespace RecipeHub.Endpoints;

public static class RecipeEndpoints
{
    public static IEndpointRouteBuilder MapRecipeEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/recipes")
            .WithTags("Recipes");
        group.MapGet("/", async (IRecipeRepository repository) =>
        {
            var recipes = await repository.GetAllAsync();
            return Results.Ok(recipes.Count>0?recipes:"No recipes");
        });
        group.MapGet("/{id}", async (IRecipeRepository repository, int id) =>
        {
            var recipe = await repository.GetByIdAsync(id);
            return Results.Ok(recipe is null?"No recipe":recipe);
        });
        group.MapPost("/", async (
            CreateRecipeDtoRequest request,
            IRecipeRepository repository) =>
        {
            var created = await repository.CreateRecipeAsync(request);

            return created is null
                ? Results.BadRequest()
                : Results.Created($"/recipes/{created.RecipeId}", created);
        });
        group.MapPut("/update-recipe/{id}", async (int id,
            UpdateRecipeDtoRequest request,
            IRecipeRepository repository) =>
        {
            var updated = await repository.UpdateRecipeAsync(id, request);

            return updated is null
                ? Results.BadRequest()
                : Results.Created($"/recipes/{updated.RecipeId}", updated);
        });
        group.MapDelete("/{id:int}", async (
            int id,
            IRecipeRepository repository) =>
        {
            var deleted = await repository.DeleteRecipeAsync(id);
            return deleted
                ? Results.Ok(new { message = "recipe deleted successfully" })
                : Results.NotFound(new { message = "Recipe not found" });
        });
        // group.MapPost("/", async (IRecipeRepository repository) =>
        // {
        //     
        // })
        return group;
    }
}