using System.ComponentModel.DataAnnotations;

namespace RecipeHub.Models;

public class Recipe
{
    [Key] public int RecipeId { get; set; }
    [MaxLength(100)] public string RecipeName { get; set; } = string.Empty;
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    [MaxLength(1000)] public string? RecipeDescription { get; set; }
    [MaxLength(100)] public string? RecipeCategory { get; set; }
    public ICollection<InstructionStep> InstructionSteps { get; set; } = new List<InstructionStep>();
    public ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}