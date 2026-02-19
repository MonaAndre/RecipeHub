using RecipeHub.DTOs.RecipeDTOs;
using RecipeHub.Models;

namespace RecipeHub.Repositories.Interfaces;

public interface IRecipeRepository
{
    Task<List<RecipeDtoResponse>> GetAllAsync();
    Task<RecipesByPageDtoResponse> GetRecipesAsync(RecipesByPageDtoRequest request);
    Task<RecipeDetailsDtoResponse?> GetByIdAsync(int id);
    Task<int> CreateRecipeAsync(CreateRecipeDtoRequest dto);
    Task<bool> UpdateRecipeAsync(int id,UpdateRecipeDtoRequest dto);
    Task<bool> DeleteRecipeAsync(int id);
    Task<bool> ValidateRecipe(int id);
}