using RecipeHub.Common;
using RecipeHub.DTOs.RecipeDTOs;
using RecipeHub.Repositories.Interfaces;
using RecipeHub.Services.Interfaces;

namespace RecipeHub.Services.Implemintations;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IProductRepository _productRepository;

    public RecipeService(IRecipeRepository recipeRepository, IProductRepository productRepository)
    {
        _recipeRepository = recipeRepository;
        _productRepository = productRepository;
    }

    public async Task<ServiceResponse<List<RecipeDtoResponse>>> GetAllRecipesAsync()
    {
        try
        {
            var recipeList = await _recipeRepository.GetAllAsync();
            return ServiceResponse<List<RecipeDtoResponse>>.SuccessResponse(recipeList,
                $"Recipes list fetched with {recipeList.Count} recipe(s)");
        }
        catch (Exception e)
        {
            return ServiceResponse<List<RecipeDtoResponse>>.ErrorResponse(e.Message, 500);
        }
    }

    public async Task<ServiceResponse<RecipesByPageDtoResponse>> GetRecipesAsync(RecipesByPageDtoRequest request)
    {
        if (request.Page < 1)
            return ServiceResponse<RecipesByPageDtoResponse>.ErrorResponse("Page must be >= 1", 400);

        if (request.PageSize < 1 || request.PageSize > 100)
            return ServiceResponse<RecipesByPageDtoResponse>.ErrorResponse("PageSize must be between 1 and 100", 400);

        try
        {
            var result = await _recipeRepository.GetRecipesAsync(request);
            return ServiceResponse<RecipesByPageDtoResponse>.SuccessResponse(result);
        }
        catch (Exception e)
        {
            return ServiceResponse<RecipesByPageDtoResponse>.ErrorResponse($"Internal server error, {e}", 500);
        }
    }

    public async Task<ServiceResponse<RecipeDetailsDtoResponse>> GetByIdAsync(int id)
    {
        try
        {
            var isValidRecipe = await _recipeRepository.ValidateRecipe(id);
            if (!isValidRecipe)
            {
                return ServiceResponse<RecipeDetailsDtoResponse>.NotFoundResponse();
            }

            var recipe = await _recipeRepository.GetByIdAsync(id);
            if (recipe is null)
            {
                return ServiceResponse<RecipeDetailsDtoResponse>.NotFoundResponse();
            }

            return ServiceResponse<RecipeDetailsDtoResponse>.SuccessResponse(recipe);
        }
        catch (Exception e)
        {
            return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse(e.Message, 500);
        }
    }

    public async Task<ServiceResponse<RecipeDetailsDtoResponse>> CreateRecipeAsync(CreateRecipeDtoRequest dto)
    {
        if (string.IsNullOrWhiteSpace(dto.RecipeName))
            return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse("RecipeName is required", 400);

        dto.RecipeName = dto.RecipeName.Trim();

        if (dto.Steps is { Count: > 0 })
        {
            foreach (var s in dto.Steps)
            {
                if (string.IsNullOrWhiteSpace(s.StepText))
                    return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse("StepText is required", 400);

                s.StepText = s.StepText.Trim();
            }
        }

        if (dto.Ingredients is { Count: > 0 })
        {
            foreach (var i in dto.Ingredients)
            {
                if (string.IsNullOrWhiteSpace(i.Unit))
                    return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse("Unit is required", 400);

                i.Unit = i.Unit.Trim();

                var isValidProduct = await _productRepository.ValidateProductAsync(i.ProductId);
                if (!isValidProduct)
                {
                    return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse($"Invalid productId: {i.ProductId}",
                        422);
                }
            }
        }

        try
        {
            var id = await _recipeRepository.CreateRecipeAsync(dto);
            var created = await _recipeRepository.GetByIdAsync(id);

            return created is null
                ? ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse("Created but not found", 500)
                : ServiceResponse<RecipeDetailsDtoResponse>.SuccessResponse(created, "Recipe created", 201);
        }
        catch (Exception)
        {
            return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse("Internal server error", 500);
        }
    }

    public async Task<ServiceResponse<RecipeDetailsDtoResponse>> UpdateRecipeAsync(int id, UpdateRecipeDtoRequest dto)
    {
        if (id <= 0)
            return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse("Id must be > 0", 400);

        if (string.IsNullOrWhiteSpace(dto.RecipeName))
            return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse("RecipeName is required", 422);

        dto.RecipeName = dto.RecipeName.Trim();

        if (dto.Steps is { Count: > 0 })
        {
            foreach (var s in dto.Steps)
            {
                if (string.IsNullOrWhiteSpace(s.StepText))
                    return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse("StepText is required", 422);

                s.StepText = s.StepText.Trim();
            }
        }

        if (dto.Ingredients is { Count: > 0 })
        {
            foreach (var i in dto.Ingredients)
            {
                if (string.IsNullOrWhiteSpace(i.Unit))
                    return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse("Unit is required", 422);

                i.Unit = i.Unit.Trim();
            }

            var productIds = dto.Ingredients.Select(x => x.ProductId).Distinct().ToList();
            var existing = await _productRepository.GetExistingProductIdsAsync(productIds);
            var invalid = productIds.Except(existing).ToList();

            if (invalid.Count > 0)
                return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse(
                    $"Invalid productIds: {string.Join(", ", invalid)}", 422);
        }

        try
        {
            var updated = await _recipeRepository.UpdateRecipeAsync(id, dto);
            if (!updated)
                return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse("Recipe not found", 404);

            var result = await _recipeRepository.GetByIdAsync(id);
            return ServiceResponse<RecipeDetailsDtoResponse>.SuccessResponse(result!);
        }
        catch (Exception e)
        {
            return ServiceResponse<RecipeDetailsDtoResponse>.ErrorResponse(e.Message, 500);
        }
    }

    public async Task<ServiceResponse<bool>> DeleteRecipeAsync(int id)
    {
        if (id <= 0)
            return ServiceResponse<bool>.ErrorResponse("Id must be > 0");
        try
        {
            var isValidRecipe = await _recipeRepository.ValidateRecipe(id);
            if (!isValidRecipe)
            {
                return ServiceResponse<bool>.ErrorResponse("Recipe not found", 404);
            }

            var isDeleted = await _recipeRepository.DeleteRecipeAsync(id);
            if (!isDeleted)
            {
                return ServiceResponse<bool>.ErrorResponse("Failed to delete recipe", 404);
            }

            return ServiceResponse<bool>.SuccessResponse(true,"Recipe deleted");
        }
        catch (Exception e)
        {
            return ServiceResponse<bool>.ErrorResponse(e.Message, 500);
        }
    }
}