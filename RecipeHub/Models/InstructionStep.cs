using System.ComponentModel.DataAnnotations;

namespace RecipeHub.Models;

public class InstructionStep
{
    [Key] public int StepId { get; set; }

    [MaxLength(400)] public string StepText { get; set; } = string.Empty;
    public int StepNumber { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
}