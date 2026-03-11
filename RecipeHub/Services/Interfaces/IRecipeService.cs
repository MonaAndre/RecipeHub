using RecipeHub.Common;
using RecipeHub.DTOs.RecipeDTOs;

namespace RecipeHub.Services.Interfaces;

public interface IRecipeService
{
    Task<ServiceResponse<List<RecipeDtoResponse>>> GetAllRecipesAsync();
     Task<ServiceResponse<RecipesByPageDtoResponse>> GetRecipesAsync(RecipesByPageDtoRequest request);
    Task<ServiceResponse<RecipeDetailsDtoResponse>> GetByIdAsync(int id);
    Task<ServiceResponse<RecipeDetailsDtoResponse>> CreateRecipeAsync(CreateRecipeDtoRequest dto, int userId);
    Task<ServiceResponse<RecipeDetailsDtoResponse>> UpdateRecipeAsync(int id, UpdateRecipeDtoRequest dto, int userId);
    Task<ServiceResponse<bool>> DeleteRecipeAsync(int id, int userId);
}