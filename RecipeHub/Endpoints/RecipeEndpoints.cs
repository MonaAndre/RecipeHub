using System.Security.Claims;
using RecipeHub.Common;
using RecipeHub.Domain;
using RecipeHub.DTOs.RecipeDTOs;

namespace RecipeHub.Endpoints;

public static class RecipeEndpoints
{
    public static IEndpointRouteBuilder MapRecipeEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/recipes")
            .WithTags("Recipes");

        group.MapGet("/all-recipes", async (IRecipeHub recipeHub) =>
        {
            var result = await recipeHub.GetAllRecipesAsync();
            return result.ToHttpResult();
        });

        app.MapGet("/paged-recipes", async (IRecipeHub recipeHub, [AsParameters] RecipesByPageDtoRequest dto) =>
        {
            var response = await recipeHub.GetRecipesAsync(dto);
            return response.ToHttpResult();
        }).AddEndpointFilter(async (context, next) =>
        {
            var dto = context.Arguments.OfType<RecipesByPageDtoRequest>().FirstOrDefault();

            if (dto is null)
                return Results.BadRequest("Missing query parameters");

            if (dto.Page is null || dto.PageSize is null)
                return Results.BadRequest("Query parameters 'page' and 'pageSize' are required.");

            if (dto.Page < 1)
                return Results.BadRequest("Page must be >= 1.");

            if (dto.PageSize < 1)
                return Results.BadRequest("PageSize must be >= 1.");

            if (dto.PageSize > 100)
                return Results.BadRequest("PageSize must be <= 100.");

            return await next(context);
        });

        group.MapGet("/{id:int}", async (IRecipeHub recipeHub, int id) =>
        {
            var result = await recipeHub.GetRecipeByIdAsync(id);
            return result.ToHttpResult();
        }).AddEndpointFilter(async (context, next) =>
        {
            var id = context.Arguments.OfType<int>().First();
            if (id <= 0)
                return Results.BadRequest("Id must be greater than 0");
            return await next(context);
        });

        // ── Kräver inloggning — userId hämtas från JWT-token ──

        group.MapPost("/create-recipe", async (
            CreateRecipeDtoRequest request,
            ClaimsPrincipal user,
            IRecipeHub recipeHub) =>
        {
            var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await recipeHub.CreateRecipeAsync(request, userId);
            return result.ToHttpResult();
        }).RequireAuthorization();

        group.MapPut("/update-recipe/{id}", async (
            int id,
            UpdateRecipeDtoRequest request,
            ClaimsPrincipal user,
            IRecipeHub recipeHub) =>
        {
            var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await recipeHub.UpdateRecipeAsync(id, request, userId);
            return result.ToHttpResult();
        }).RequireAuthorization();

        group.MapDelete("/{id:int}", async (
            int id,
            ClaimsPrincipal user,
            IRecipeHub recipeHub) =>
        {
            var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await recipeHub.DeleteRecipeAsync(id, userId);
            return result.ToHttpResult();
        }).RequireAuthorization();

        return group;
    }
}
