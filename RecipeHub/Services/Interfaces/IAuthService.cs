using RecipeHub.Common;
using RecipeHub.DTOs.AuthDTOs;

namespace RecipeHub.Services.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse<LoginDtoResponse>> LoginAsync(LoginDtoRequest request);
    Task<ServiceResponse<LoginDtoResponse>> RegisterAsync(RegisterDtoRequest request);
}