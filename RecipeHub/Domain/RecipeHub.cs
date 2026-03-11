using RecipeHub.Common;
using RecipeHub.DTOs.ProductDTOs;
using RecipeHub.DTOs.RecipeDTOs;
using RecipeHub.Models;
using RecipeHub.Repositories.Interfaces;
using RecipeHub.Services.Interfaces;

namespace RecipeHub.Domain;

public class RecipeHub : IRecipeHub
{
    private readonly IProductService _productService;
    private readonly IRecipeService _recipeService;

    public RecipeHub(IProductService productService, IRecipeService recipeService)
    {
        _productService = productService;
        _recipeService = recipeService;
    }

    public async Task<ServiceResponse<List<ProductDtoResponse>>> GetAllProductsAsync()
    {
        return await _productService.GetAllProductsAsync();
    }

    public async Task<ServiceResponse<ProductDtoResponse>> CreateProductAsync(ProductDtoRequest dto)
    {
        return await _productService.CreateProductAsync(dto);
    }

    public async Task<ServiceResponse<ProductDtoResponse>> UpdateProductAsync(int id, ProductDtoRequest dto)
    {
        return await _productService.UpdateProductAsync(id, dto);
    }

    public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
    {
        return await _productService.DeleteProductAsync(id);
    }

    public async Task<ServiceResponse<List<RecipeDtoResponse>>> GetAllRecipesAsync()
    {
        return await _recipeService.GetAllRecipesAsync();
    }

    public async Task<ServiceResponse<RecipesByPageDtoResponse>> GetRecipesAsync(RecipesByPageDtoRequest request)
    {
        return await _recipeService.GetRecipesAsync(request);
    }

    public async Task<ServiceResponse<RecipeDetailsDtoResponse>> GetRecipeByIdAsync(int id)
    {
        return await _recipeService.GetByIdAsync(id);
    }

    public async Task<ServiceResponse<RecipeDetailsDtoResponse>> CreateRecipeAsync(CreateRecipeDtoRequest dto, int userId)
    {
        return await _recipeService.CreateRecipeAsync(dto, userId);
    }

    public async Task<ServiceResponse<RecipeDetailsDtoResponse>> UpdateRecipeAsync(int id, UpdateRecipeDtoRequest dto, int userId)
    {
        return await _recipeService.UpdateRecipeAsync(id, dto, userId);
    }

    public async Task<ServiceResponse<bool>> DeleteRecipeAsync(int id, int userId)
    {
        return await _recipeService.DeleteRecipeAsync(id, userId);
    }

}