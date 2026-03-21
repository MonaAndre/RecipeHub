using RecipeHub.Common;
using RecipeHub.DTOs.CommentDTOs;
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
    Task<ServiceResponse<RecipeDetailsDtoResponse>> CreateRecipeAsync(CreateRecipeDtoRequest dto, int userId);
    Task<ServiceResponse<RecipeDetailsDtoResponse>> UpdateRecipeAsync(int id, UpdateRecipeDtoRequest dto, int userId);
    Task<ServiceResponse<bool>> DeleteRecipeAsync(int id, int userId);
    
    Task<ServiceResponse<List<CommentDtoResponse>>> GetCommentsByRecipeIdAsync(int recipeId);
    Task<ServiceResponse<CommentDtoResponse>> CreateCommentAsync(int recipeId, int userId, CreateCommentDtoRequest dto);
    Task<ServiceResponse<CommentDtoResponse>> UpdateCommentAsync(int commentId, int userId, UpdateCommentDtoRequest dto);
    Task<ServiceResponse<bool>> DeleteCommentAsync(int commentId, int userId);
}