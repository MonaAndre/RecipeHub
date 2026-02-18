using RecipeHub.DTOs.ProductDTOs;
using RecipeHub.Models;
using RecipeHub.Repositories.Interfaces;

namespace RecipeHub.Domain;

public class RecipeHub
{
    private readonly IProductRepository  _productRepository;
    private readonly IRecipeRepository _recipeRepository;

    public RecipeHub(IProductRepository  productRepository, IRecipeRepository recipeRepository)
    {
        _productRepository = productRepository;
        _recipeRepository = recipeRepository;
    }

    public async Task<List<ProductDtoResponse>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }
}