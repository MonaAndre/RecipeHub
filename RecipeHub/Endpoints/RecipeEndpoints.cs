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
        // group.MapPost("/", async (IRecipeRepository repository) =>
        // {
        //     
        // })
        return group;
    }
}