using System.Security.Claims;
using RecipeHub.Common;
using RecipeHub.Domain;
using RecipeHub.DTOs.CommentDTOs;

namespace RecipeHub.Endpoints;

public static class CommentEndpoints
{
    public static IEndpointRouteBuilder MapCommentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/recipes/{recipeId:int}/comments")
            .WithTags("Comments");
        
        group.MapGet("/", async (int recipeId, IRecipeHub recipeHub) =>
        {
            var result = await recipeHub.GetCommentsByRecipeIdAsync(recipeId);
            return result.ToHttpResult();
        });

        group.MapPost("/", async (
            int recipeId,
            CreateCommentDtoRequest dto,
            ClaimsPrincipal user,
            IRecipeHub recipeHub) =>
        {
            var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await recipeHub.CreateCommentAsync(recipeId, userId, dto);
            return result.ToHttpResult();
        }).RequireAuthorization();

        group.MapPut("/{commentId:int}", async (
            int recipeId,
            int commentId,
            UpdateCommentDtoRequest dto,
            ClaimsPrincipal user,
            IRecipeHub recipeHub) =>
        {
            var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await recipeHub.UpdateCommentAsync(commentId, userId, dto);
            return result.ToHttpResult();
        }).RequireAuthorization();

        group.MapDelete("/{commentId:int}", async (
            int recipeId,
            int commentId,
            ClaimsPrincipal user,
            IRecipeHub recipeHub) =>
        {
            var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await recipeHub.DeleteCommentAsync(commentId, userId);
            return result.ToHttpResult();
        }).RequireAuthorization();

        return group;
    }
}
