namespace RecipeHub.DTOs.RecipeDTOs;

public class UpdateRecipeDtoRequest
{
    public string RecipeName { get; set; } = string.Empty;
    public string? RecipeDescription { get; set; }
}