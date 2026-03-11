namespace RecipeHub.DTOs.RecipeDTOs;

public class RecipeDtoResponse
{
    public int Id { get; set; }
    public string RecipeName { get; set; } = string.Empty;
    public string RecipeCategory { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}