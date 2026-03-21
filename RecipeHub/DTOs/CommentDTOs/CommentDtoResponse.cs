namespace RecipeHub.DTOs.CommentDTOs;

public class CommentDtoResponse
{
    public int CommentId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int RecipeId { get; set; }
    public DateTime CreatedAt { get; set; }
}
