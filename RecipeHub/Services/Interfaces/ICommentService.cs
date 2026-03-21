using RecipeHub.Common;
using RecipeHub.DTOs.CommentDTOs;

namespace RecipeHub.Services.Interfaces;

public interface ICommentService
{
    Task<ServiceResponse<List<CommentDtoResponse>>> GetByRecipeIdAsync(int recipeId);
    Task<ServiceResponse<CommentDtoResponse>> CreateAsync(int recipeId, int userId, CreateCommentDtoRequest dto);
    Task<ServiceResponse<CommentDtoResponse>> UpdateAsync(int commentId, int userId, UpdateCommentDtoRequest dto);
    Task<ServiceResponse<bool>> DeleteAsync(int commentId, int userId);
}
