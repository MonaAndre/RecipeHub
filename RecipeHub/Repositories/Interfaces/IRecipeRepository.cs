using RecipeHub.DTOs.RecipeDTOs;
using RecipeHub.Models;

namespace RecipeHub.Repositories.Interfaces;

public interface IRecipeRepository
{
    Task<List<RecipeDtoResponse>> GetAllAsync();
    Task<RecipesByPageDtoResponse> GetRecipesAsync(RecipesByPageDtoRequest request);
    Task<RecipeDetailsDtoResponse?> GetByIdAsync(int id);
    Task<int> CreateRecipeAsync(CreateRecipeDtoRequest dto, int userId);
    Task<bool> UpdateRecipeAsync(int id, UpdateRecipeDtoRequest dto, int userId);
    Task<bool> DeleteRecipeAsync(int id, int userId);
    Task<bool> ValidateRecipe(int id);
}