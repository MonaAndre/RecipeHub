using RecipeHub.Common;
using RecipeHub.DTOs.ProductDTOs;
using RecipeHub.Models;
using RecipeHub.Repositories.Interfaces;
using RecipeHub.Services.Interfaces;

namespace RecipeHub.Domain;

public class RecipeHub : IRecipeHub
{
    private readonly IProductService _productService;

    public RecipeHub(IProductService productService)
    {
        _productService = productService;
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

}