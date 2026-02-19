using RecipeHub.Common;
using RecipeHub.DTOs.ProductDTOs;
using RecipeHub.DTOs.RecipeDTOs;

namespace RecipeHub.Domain;

public interface IRecipeHub
{
    Task<ServiceResponse<List<ProductDtoResponse>>> GetAllProductsAsync();
    Task<ServiceResponse<ProductDtoResponse>> CreateProductAsync(ProductDtoRequest dto);
    Task<ServiceResponse<ProductDtoResponse>> UpdateProductAsync(int id, ProductDtoRequest dto);
    Task<ServiceResponse<bool>> DeleteProductAsync(int id);
    
    
    Task<ServiceResponse<List<RecipeDtoResponse>>> GetAllRecipesAsync();
    Task<ServiceResponse<RecipesByPageDtoResponse>> GetRecipesAsync(RecipesByPageDtoRequest request);
     Task<ServiceResponse<RecipeDetailsDtoResponse>> GetRecipeByIdAsync(int id);
     Task<ServiceResponse<RecipeDetailsDtoResponse>> CreateRecipeAsync(CreateRecipeDtoRequest dto);
     Task<ServiceResponse<RecipeDetailsDtoResponse>> UpdateRecipeAsync(int id,UpdateRecipeDtoRequest dto);
    Task<ServiceResponse<bool>> DeleteRecipeAsync(int id);
}