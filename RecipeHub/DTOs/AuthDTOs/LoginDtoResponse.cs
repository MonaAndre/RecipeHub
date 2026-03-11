namespace RecipeHub.DTOs.AuthDTOs;

public class LoginDtoResponse
{
    public string Token { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int UserId { get; set; }
}