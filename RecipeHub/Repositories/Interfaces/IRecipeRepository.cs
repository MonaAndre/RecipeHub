using RecipeHub.DTOs.RecipeDTOs;
using RecipeHub.Models;

namespace RecipeHub.Repositories.Interfaces;

public interface IRecipeRepository
{
    Task<List<RecipeDtoResponse>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(int id);
    Task<bool> CreateRecipeAsync(Recipe recipe);
    Task<bool> UpdateRecipeAsync(int id,UpdateRecipeDtoRequest dto);
    Task<bool> DeleteRecipeAsync(int id);
}