namespace RecipeHub.DTOs.RecipeDTOs;

public class CreateRecipeDtoRequest
{
    public string RecipeName { get; init; } = null!;
    public string Description { get; init; } = null!;
}