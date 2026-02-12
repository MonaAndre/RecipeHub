using Microsoft.EntityFrameworkCore;
using RecipeHub.Data;
using RecipeHub.DTOs.RecipeDTOs;
using RecipeHub.Models;
using RecipeHub.Repositories.Interfaces;

namespace RecipeHub.Repositories.Implementations;

public class RecipeRepository : IRecipeRepository
{
    private readonly AppDbContext _context;

    public RecipeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RecipeDtoResponse>> GetAllAsync()
    {
        return await _context.Recipes.Select(r => new RecipeDtoResponse
        {
            RecipeName = r.RecipeName,
            RecipeCategory = r.RecipeCategory!
        }).ToListAsync();
    }

    public async Task<RecipeDetailsDtoResponse?> GetByIdAsync(int id)
    {
        var recipe = await _context.Recipes
            .Where(r => r.RecipeId == id)
            .Select(r => new RecipeDetailsDtoResponse
            {
                RecipeId = r.RecipeId,
                RecipeName = r.RecipeName,
                RecipeDescription = r.RecipeDescription,
                RecipeCategory = r.RecipeCategory,
                InstructionSteps = r.InstructionSteps
                    .OrderBy(s => s.StepNumber)
                    .Select(s => new InstructionStepDto
                    {
                        StepId = s.StepId,
                        StepNumber = s.StepNumber,
                        StepText = s.StepText
                    }).ToList(),

                Ingredients = r.Ingredients
                    .Select(i=> new RecipeIngredientDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product.ProductNamn,
                        Quantity = i.Quantity,
                        Unit = i.Unit
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        return recipe;
    }

    public async Task<RecipeDetailsDtoResponse?> CreateRecipeAsync(CreateRecipeDtoRequest dto)
    {
        if (string.IsNullOrWhiteSpace(dto.RecipeName))
            return null;
        await using var trans = await _context.Database.BeginTransactionAsync();
        
        var recipe = new Recipe
        {
            RecipeName = dto.RecipeName.Trim(),
            RecipeDescription = dto.RecipeDescription,
            RecipeCategory = dto.RecipeCategory
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        if (dto.Steps?.Count > 0)
        {
            var steps = dto.Steps
                .Select((s, index) => new InstructionStep
                {
                    RecipeId = recipe.RecipeId,
                    StepText = s.StepText.Trim(),
                    StepNumber = index + 1
                })
                .ToList();

            _context.InstructionSteps.AddRange(steps);

        }
        if (dto.Ingredients is { Count: > 0 })
        {
            var ingredients = dto.Ingredients.Select(i => new RecipeIngredient
            {
                RecipeId = recipe.RecipeId,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Unit = i.Unit.Trim()
            });

            _context.RecipeIngredients.AddRange(ingredients);
        }

        await _context.SaveChangesAsync();
        await trans.CommitAsync();

        // Return response
        var response = await _context.Recipes
            .Where(r => r.RecipeId == recipe.RecipeId)
            .Select(r => new RecipeDetailsDtoResponse
            {
                RecipeId = r.RecipeId,
                RecipeName = r.RecipeName,
                RecipeDescription = r.RecipeDescription,
                RecipeCategory = r.RecipeCategory,
                InstructionSteps = r.InstructionSteps
                    .OrderBy(s => s.StepNumber)
                    .Select(s => new InstructionStepDto
                    {
                        StepId = s.StepId,
                        StepNumber = s.StepNumber,
                        StepText = s.StepText
                    }).ToList(),
                Ingredients = r.Ingredients
                    .Select(i => new RecipeIngredientDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product.ProductNamn,
                        Quantity = i.Quantity,
                        Unit = i.Unit
                    }).ToList()
            }).FirstAsync();
        return response;
    }

    public async Task<RecipeDetailsDtoResponse?> UpdateRecipeAsync(int id, UpdateRecipeDtoRequest dto)
    {
        var recipe = await _context.Recipes
            .Include(r => r.InstructionSteps)
            .Include(r => r.Ingredients)
            .FirstOrDefaultAsync(r => r.RecipeId == id);

        if (recipe is null)
            return null;

        recipe.RecipeName = dto.RecipeName.Trim();
        recipe.RecipeDescription = dto.RecipeDescription;
        recipe.RecipeCategory = dto.RecipeCategory;

        _context.InstructionSteps.RemoveRange(recipe.InstructionSteps);

        if (dto.Steps?.Count > 0)
        {
            var newSteps = dto.Steps
                .Select((s, index) => new InstructionStep
                {
                    RecipeId = recipe.RecipeId,
                    StepText = s.StepText.Trim(),
                    StepNumber = index + 1
                });

            await _context.InstructionSteps.AddRangeAsync(newSteps);
        }

        _context.RecipeIngredients.RemoveRange(recipe.Ingredients);

        if (dto.Ingredients?.Count > 0)
        {
            var newIngredients = dto.Ingredients
                .Select(i => new RecipeIngredient
                {
                    RecipeId = recipe.RecipeId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Unit = i.Unit.Trim()
                });

            await _context.RecipeIngredients.AddRangeAsync(newIngredients);
        }

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteRecipeAsync(int id)
    {
        var recipeToDelete = await _context.Recipes.FirstOrDefaultAsync(r => r.RecipeId == id);
        if (recipeToDelete == null) return false;
        _context.Recipes.Remove(recipeToDelete);
        return await _context.SaveChangesAsync() > 0;
    }
}