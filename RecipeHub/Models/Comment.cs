using System.ComponentModel.DataAnnotations;

namespace RecipeHub.Models;

public class Comment
{
    [Key] public int CommentId { get; set; }

    [MaxLength(1000)] public string Text { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}