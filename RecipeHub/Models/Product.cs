using System.ComponentModel.DataAnnotations;

namespace RecipeHub.Models;

public class Product
{
    [Key] public int ProductId { get; set; }
    [MaxLength(100)] public string ProductNamn { get; set; } = string.Empty;
    [MaxLength(100)] public string? Category { get; set; }
    ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}