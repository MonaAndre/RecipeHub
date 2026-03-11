using RecipeHub.Common;
using RecipeHub.DTOs.AuthDTOs;
using RecipeHub.Services.Interfaces;

namespace RecipeHub.Endpoints;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth")
            .WithTags("Auth");

        group.MapPost("/login", async (LoginDtoRequest request, IAuthService authService) =>
        {
            var response = await authService.LoginAsync(request);
            return response.ToHttpResult();
        });

        group.MapPost("/register", async (RegisterDtoRequest request, IAuthService authService) =>
        {
            var response = await authService.RegisterAsync(request);
            return response.ToHttpResult();
        });
        return group;
    }
}