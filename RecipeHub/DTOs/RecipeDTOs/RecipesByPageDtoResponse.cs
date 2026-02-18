namespace RecipeHub.DTOs.RecipeDTOs;

public class RecipesByPageDtoResponse
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<RecipeDtoResponse> Recipes { get; set; }
}