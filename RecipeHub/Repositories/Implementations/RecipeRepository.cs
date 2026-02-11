using Microsoft.EntityFrameworkCore;
using RecipeHub.Data;
using RecipeHub.DTOs.RecipeDTOs;
using RecipeHub.Models;
using RecipeHub.Repositories.Interfaces;

namespace RecipeHub.Repositories.Implementations;

public class RecipeRepository : IRecipeRepository
{
    private readonly AppDbContext _constext;

    public RecipeRepository(AppDbContext constext)
    {
        _constext = constext;
    }

    public async Task<List<RecipeDtoResponse>> GetAllAsync()
    {
        return await _constext.Recipes.Select(r => new RecipeDtoResponse
        {
            RecipeName = r.RecipeName,
            RecipeCategory = r.RecipeCategory!
        }).ToListAsync();
    }

    public async Task<Recipe?> GetByIdAsync(int id)
    {
        var recipe = await _constext.Recipes.FirstOrDefaultAsync(r => r.RecipeId == id);
        return recipe;
    }

    public async Task<bool> CreateRecipeAsync(Recipe recipe)
    {
        await _constext.Recipes.AddAsync(recipe);
        return await _constext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateRecipeAsync(int id, UpdateRecipeDtoRequest dto)
    {
        var recipe = await _constext.Recipes.FirstOrDefaultAsync(r => r.RecipeId == id);
        recipe!.RecipeName = dto.RecipeName;
        recipe.RecipeDescription = dto.RecipeDescription;
        return await _constext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteRecipeAsync(int id)
    {
        var recipeToDelete = await _constext.Recipes.FirstOrDefaultAsync(r => r.RecipeId == id);
        if (recipeToDelete == null) return false;
        _constext.Recipes.Remove(recipeToDelete);
        return await _constext.SaveChangesAsync() > 0;
    }
}