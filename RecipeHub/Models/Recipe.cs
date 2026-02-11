using System.ComponentModel.DataAnnotations;

namespace RecipeHub.Models;

public class Recipe
{
    [Key] public int RecipeId { get; set; }
    [MaxLength(100)] public string RecipeName { get; set; } = string.Empty;
    [MaxLength(1000)] public string? RecipeDescription { get; set; }
    [MaxLength(100)] public string? RecipeCategory { get; set; }
    ICollection<Product> Ingredients { get; set; } = new List<Product>();
}