namespace RecipeHub.DTOs.RecipeDTOs;

public class CreateRecipeDtoRequest
{
    public string RecipeName { get; set; } = string.Empty;
    public string? RecipeDescription { get; set; }
    public string? RecipeCategory { get; set; }
    public List<CreateStepDtoRequest>? Steps { get; set; }
    public List<CreateIngredientDtoRequest>? Ingredients { get; set; }
}

public class CreateStepDtoRequest
{
    public int StepId { get; set; }
    public int? StepNumber { get; set; }
    public string StepText { get; set; } = string.Empty;
}

public class CreateIngredientDtoRequest
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
}