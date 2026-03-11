using System.ComponentModel.DataAnnotations;

namespace RecipeHub.Models;

public class User
{
    [Key] public int UserId { get; set; }
    [MaxLength(50)] public string UserName { get; set; } = string.Empty;
     public string PasswordHash { get; set; } = string.Empty;
    [MaxLength(200)] public string Email { get; set; } = string.Empty;
    [MaxLength(100)] public string FirstName { get; set; } = string.Empty;
    [MaxLength(100)] public string LastName { get; set; } = string.Empty;
    public DateTime? Birthdate { get; set; }
    public bool IsConfirmed { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}