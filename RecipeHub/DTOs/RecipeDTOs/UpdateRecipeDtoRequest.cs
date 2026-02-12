namespace RecipeHub.DTOs.RecipeDTOs;

public class UpdateRecipeDtoRequest
{
    public string RecipeName { get; set; } = string.Empty;
    public string? RecipeDescription { get; set; }
    public string? RecipeCategory { get; set; }

    public List<CreateStepDtoRequest>? Steps { get; set; }
    public List<CreateIngredientDtoRequest>? Ingredients { get; set; }
}