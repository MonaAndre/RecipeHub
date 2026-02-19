namespace RecipeHub.DTOs.RecipeDTOs;

public class RecipesByPageDtoRequest
{
   public int? Page { get; set; }
   public int? PageSize { get; set; }
   public string? Search { get; set; }
}