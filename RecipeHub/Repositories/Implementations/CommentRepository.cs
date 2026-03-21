using Microsoft.EntityFrameworkCore;
using RecipeHub.Data;
using RecipeHub.DTOs.CommentDTOs;
using RecipeHub.Models;
using RecipeHub.Repositories.Interfaces;

namespace RecipeHub.Repositories.Implementations;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CommentDtoResponse>> GetByRecipeIdAsync(int recipeId)
    {
        return await _context.Comments
            .Where(c => c.RecipeId == recipeId)
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new CommentDtoResponse
            {
                CommentId = c.CommentId,
                Text      = c.Text,
                UserName  = c.User.UserName,
                UserId    = c.UserId,
                RecipeId  = c.RecipeId,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<CommentDtoResponse?> GetByIdAsync(int commentId)
    {
        return await _context.Comments
            .Where(c => c.CommentId == commentId)
            .Select(c => new CommentDtoResponse
            {
                CommentId = c.CommentId,
                Text      = c.Text,
                UserName  = c.User.UserName,
                UserId    = c.UserId,
                RecipeId  = c.RecipeId,
                CreatedAt = c.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CommentDtoResponse> CreateAsync(int recipeId, int userId, CreateCommentDtoRequest dto)
    {
        var comment = new Comment
        {
            Text      = dto.Text.Trim(),
            RecipeId  = recipeId,
            UserId    = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return (await GetByIdAsync(comment.CommentId))!;
    }

    public async Task<bool> UpdateAsync(int commentId, int userId, UpdateCommentDtoRequest dto)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId);

        if (comment is null) return false;

        if (comment.UserId != userId) return false;

        comment.Text = dto.Text.Trim();
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int commentId, int userId)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId);

        if (comment is null) return false;

        if (comment.UserId != userId) return false;

        _context.Comments.Remove(comment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ExistsAsync(int commentId)
    {
        return await _context.Comments.AnyAsync(c => c.CommentId == commentId);
    }
}
