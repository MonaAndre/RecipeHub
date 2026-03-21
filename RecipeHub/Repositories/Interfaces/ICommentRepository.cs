using RecipeHub.DTOs.CommentDTOs;

namespace RecipeHub.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<List<CommentDtoResponse>> GetByRecipeIdAsync(int recipeId);
    Task<CommentDtoResponse?> GetByIdAsync(int commentId);
    Task<CommentDtoResponse> CreateAsync(int recipeId, int userId, CreateCommentDtoRequest dto);
    Task<bool> UpdateAsync(int commentId, int userId, UpdateCommentDtoRequest dto);
    Task<bool> DeleteAsync(int commentId, int userId);
    Task<bool> ExistsAsync(int commentId);
}
