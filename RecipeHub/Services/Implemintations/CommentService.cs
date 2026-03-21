using RecipeHub.Common;
using RecipeHub.DTOs.CommentDTOs;
using RecipeHub.Repositories.Interfaces;
using RecipeHub.Services.Interfaces;

namespace RecipeHub.Services.Implemintations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IRecipeRepository  _recipeRepository;

    public CommentService(ICommentRepository commentRepository, IRecipeRepository recipeRepository)
    {
        _commentRepository = commentRepository;
        _recipeRepository  = recipeRepository;
    }

    public async Task<ServiceResponse<List<CommentDtoResponse>>> GetByRecipeIdAsync(int recipeId)
    {
        try
        {
            var recipeExists = await _recipeRepository.ValidateRecipe(recipeId);
            if (!recipeExists)
                return ServiceResponse<List<CommentDtoResponse>>.NotFoundResponse("Recipe not found");

            var comments = await _commentRepository.GetByRecipeIdAsync(recipeId);
            return ServiceResponse<List<CommentDtoResponse>>.SuccessResponse(comments,
                $"{comments.Count} comment(s) found");
        }
        catch (Exception e)
        {
            return ServiceResponse<List<CommentDtoResponse>>.ErrorResponse(e.Message, 500);
        }
    }

    public async Task<ServiceResponse<CommentDtoResponse>> CreateAsync(int recipeId, int userId, CreateCommentDtoRequest dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Text))
            return ServiceResponse<CommentDtoResponse>.ErrorResponse("Comment text is required", 400);

        if (dto.Text.Length > 1000)
            return ServiceResponse<CommentDtoResponse>.ErrorResponse("Comment text is too long (max 1000 characters)", 400);

        try
        {
            var recipeExists = await _recipeRepository.ValidateRecipe(recipeId);
            if (!recipeExists)
                return ServiceResponse<CommentDtoResponse>.NotFoundResponse("Recipe not found");

            var comment = await _commentRepository.CreateAsync(recipeId, userId, dto);
            return ServiceResponse<CommentDtoResponse>.SuccessResponse(comment, "Comment created", 201);
        }
        catch (Exception e)
        {
            return ServiceResponse<CommentDtoResponse>.ErrorResponse(e.Message, 500);
        }
    }

    public async Task<ServiceResponse<CommentDtoResponse>> UpdateAsync(int commentId, int userId, UpdateCommentDtoRequest dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Text))
            return ServiceResponse<CommentDtoResponse>.ErrorResponse("Comment text is required", 400);

        if (dto.Text.Length > 1000)
            return ServiceResponse<CommentDtoResponse>.ErrorResponse("Comment text is too long (max 1000 characters)", 400);

        try
        {
            var exists = await _commentRepository.ExistsAsync(commentId);
            if (!exists)
                return ServiceResponse<CommentDtoResponse>.NotFoundResponse("Comment not found");

            var updated = await _commentRepository.UpdateAsync(commentId, userId, dto);
            if (!updated)
                return ServiceResponse<CommentDtoResponse>.ForbiddenResponse("You can only edit your own comments");

            var result = await _commentRepository.GetByIdAsync(commentId);
            return ServiceResponse<CommentDtoResponse>.SuccessResponse(result!);
        }
        catch (Exception e)
        {
            return ServiceResponse<CommentDtoResponse>.ErrorResponse(e.Message, 500);
        }
    }

    public async Task<ServiceResponse<bool>> DeleteAsync(int commentId, int userId)
    {
        try
        {
            var exists = await _commentRepository.ExistsAsync(commentId);
            if (!exists)
                return ServiceResponse<bool>.NotFoundResponse();

            var deleted = await _commentRepository.DeleteAsync(commentId, userId);
            if (!deleted)
                return ServiceResponse<bool>.ForbiddenResponse("You can only delete your own comments");

            return ServiceResponse<bool>.SuccessResponse(true, "Comment deleted");
        }
        catch (Exception e)
        {
            return ServiceResponse<bool>.ErrorResponse(e.Message, 500);
        }
    }
}
