using RecipeHub.Models;

namespace RecipeHub.DTOs.RecipeDTOs;

public class RecipeDetailsDtoResponse
{
    public int RecipeId { get; set; }
    public string RecipeName { get; set; } = string.Empty;
    public string? RecipeDescription { get; set; }
    public string? RecipeCategory { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    public List<InstructionStepDto> InstructionSteps { get; set; } = new();
    public List<RecipeIngredientDto> Ingredients { get; set; } = new();
}

public class InstructionStepDto
{
    public int StepId { get; set; }
    public int StepNumber { get; set; }
    public string StepText { get; set; } = string.Empty;
}

public class RecipeIngredientDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
}