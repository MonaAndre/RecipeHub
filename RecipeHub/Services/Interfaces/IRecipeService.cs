using RecipeHub.DTOs.RecipeDTOs;

namespace RecipeHub.Services.Interfaces;

public interface IRecipeService
{
    Task<List<RecipeDtoResponse>> GetAllAsync();
    Task<RecipesByPageDtoResponse> GetRecipesAsync(RecipesByPageDtoRequest request);
    Task<RecipeDetailsDtoResponse?> GetByIdAsync(int id);
    Task<RecipeDetailsDtoResponse?> CreateRecipeAsync(CreateRecipeDtoRequest dto);
    Task<RecipeDetailsDtoResponse?> UpdateRecipeAsync(int id,UpdateRecipeDtoRequest dto);
    Task<bool> DeleteRecipeAsync(int id);
}