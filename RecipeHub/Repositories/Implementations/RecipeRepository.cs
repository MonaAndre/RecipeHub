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
            Id             = r.RecipeId,
            RecipeName     = r.RecipeName,
            RecipeCategory = r.RecipeCategory!,
            UserName       = r.User.UserName 
        }).ToListAsync();
    }

    public async Task<RecipesByPageDtoResponse> GetRecipesAsync(RecipesByPageDtoRequest request)
    {
        var page     = Math.Max(request.Page ?? 1, 1);
        var pageSize = Math.Clamp(request.PageSize ?? 5, 1, 100);

        var query = _context.Recipes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(r => r.RecipeName.Contains(request.Search));

        var totalCount = await query.CountAsync();

        var recipes = await query
            .OrderBy(r => r.RecipeId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new RecipeDtoResponse
            {
                Id             = r.RecipeId,
                RecipeName     = r.RecipeName,
                RecipeCategory = r.RecipeCategory!,
                UserName       = r.User.UserName
            })
            .ToListAsync();

        return new RecipesByPageDtoResponse
        {
            Page       = page,
            PageSize   = pageSize,
            TotalCount = totalCount,
            Recipes    = recipes
        };
    }

    public async Task<RecipeDetailsDtoResponse?> GetByIdAsync(int id)
    {
        return await _context.Recipes
            .Where(r => r.RecipeId == id)
            .Select(r => new RecipeDetailsDtoResponse
            {
                RecipeId          = r.RecipeId,
                RecipeName        = r.RecipeName,
                RecipeDescription = r.RecipeDescription,
                RecipeCategory    = r.RecipeCategory,
                UserId            = r.UserId,
                UserName          = r.User.UserName,
                InstructionSteps  = r.InstructionSteps
                    .OrderBy(s => s.StepNumber)
                    .Select(s => new InstructionStepDto
                    {
                        StepId     = s.StepId,
                        StepNumber = s.StepNumber,
                        StepText   = s.StepText
                    }).ToList(),
                Ingredients = r.Ingredients
                    .Select(i => new RecipeIngredientDto
                    {
                        ProductId   = i.ProductId,
                        ProductName = i.Product.ProductNamn,
                        Quantity    = i.Quantity,
                        Unit        = i.Unit
                    }).ToList(),
                Comments = r.Comments 
                    .OrderByDescending(c => c.CreatedAt)
                    .Select(c => new CommentDto
                    {
                        CommentId = c.CommentId,
                        Text      = c.Text,
                        UserName  = c.User.UserName,
                        CreatedAt = c.CreatedAt
                    }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> CreateRecipeAsync(CreateRecipeDtoRequest dto, int userId)
    {
        await using var trans = await _context.Database.BeginTransactionAsync();

        var recipe = new Recipe
        {
            RecipeName        = dto.RecipeName!,
            RecipeDescription = dto.RecipeDescription,
            RecipeCategory    = dto.RecipeCategory,
            UserId            = userId
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        if (dto.Steps?.Count > 0)
        {
            var steps = dto.Steps
                .Select((s, index) => new InstructionStep
                {
                    RecipeId   = recipe.RecipeId,
                    StepText   = s.StepText.Trim(),
                    StepNumber = index + 1
                })
                .ToList();
            _context.InstructionSteps.AddRange(steps);
        }

        if (dto.Ingredients is { Count: > 0 })
        {
            var ingredients = dto.Ingredients.Select(i => new RecipeIngredient
            {
                RecipeId  = recipe.RecipeId,
                ProductId = i.ProductId,
                Quantity  = i.Quantity,
                Unit      = i.Unit
            });
            _context.RecipeIngredients.AddRange(ingredients);
        }

        await _context.SaveChangesAsync();
        await trans.CommitAsync();
        return recipe.RecipeId;
    }

    public async Task<bool> UpdateRecipeAsync(int id, UpdateRecipeDtoRequest dto, int userId)
    {
        var recipe = await _context.Recipes
            .Include(r => r.InstructionSteps)
            .Include(r => r.Ingredients)
            .FirstOrDefaultAsync(r => r.RecipeId == id);

        if (recipe is null) return false;

        // Ownership check — only the creator can update
        if (recipe.UserId != userId) return false;

        recipe.RecipeName        = dto.RecipeName!;
        recipe.RecipeDescription = dto.RecipeDescription;
        recipe.RecipeCategory    = dto.RecipeCategory;

        _context.InstructionSteps.RemoveRange(recipe.InstructionSteps);
        if (dto.Steps is { Count: > 0 })
        {
            var newSteps = dto.Steps.Select((s, index) => new InstructionStep
            {
                RecipeId   = recipe.RecipeId,
                StepText   = s.StepText!,
                StepNumber = index + 1
            });
            await _context.InstructionSteps.AddRangeAsync(newSteps);
        }

        _context.RecipeIngredients.RemoveRange(recipe.Ingredients);
        if (dto.Ingredients is { Count: > 0 })
        {
            var newIngredients = dto.Ingredients.Select(i => new RecipeIngredient
            {
                RecipeId  = recipe.RecipeId,
                ProductId = i.ProductId,
                Quantity  = i.Quantity,
                Unit      = i.Unit!
            });
            await _context.RecipeIngredients.AddRangeAsync(newIngredients);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteRecipeAsync(int id, int userId)
    {
        var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.RecipeId == id);
        if (recipe is null) return false;

        // Ownership check — only the creator can delete
        if (recipe.UserId != userId) return false;

        _context.Recipes.Remove(recipe);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ValidateRecipe(int id)
    {
        return await _context.Recipes.AnyAsync(r => r.RecipeId == id);
    }
}
