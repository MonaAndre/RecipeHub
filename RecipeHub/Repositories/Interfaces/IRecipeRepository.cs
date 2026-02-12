using RecipeHub.DTOs.RecipeDTOs;
using RecipeHub.Models;

namespace RecipeHub.Repositories.Interfaces;

public interface IRecipeRepository
{
    Task<List<RecipeDtoResponse>> GetAllAsync();
    Task<RecipeDetailsDtoResponse?> GetByIdAsync(int id);
    Task<RecipeDetailsDtoResponse?> CreateRecipeAsync(CreateRecipeDtoRequest dto);
    Task<RecipeDetailsDtoResponse?> UpdateRecipeAsync(int id,UpdateRecipeDtoRequest dto);
    Task<bool> DeleteRecipeAsync(int id);
}