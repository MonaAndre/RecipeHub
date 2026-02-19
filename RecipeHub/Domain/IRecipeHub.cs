using RecipeHub.Common;
using RecipeHub.DTOs.ProductDTOs;

namespace RecipeHub.Domain;

public interface IRecipeHub
{
    Task<ServiceResponse<List<ProductDtoResponse>>> GetAllProductsAsync();
    Task<ServiceResponse<ProductDtoResponse>> CreateProductAsync(ProductDtoRequest dto);
    Task<ServiceResponse<ProductDtoResponse>> UpdateProductAsync(int id, ProductDtoRequest dto);
    Task<ServiceResponse<bool>> DeleteProductAsync(int id);
}