using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RecipeHub.Common;
using RecipeHub.DTOs;
using RecipeHub.DTOs.AuthDTOs;
using RecipeHub.Repositories.Interfaces;
using RecipeHub.Services.Interfaces;

namespace RecipeHub.Services.Implemintations;

public class AuthService(IUserRepository userRepo, IConfiguration config) : IAuthService
{
    public async Task<ServiceResponse<LoginDtoResponse>> LoginAsync(LoginDtoRequest request)
    {
        var user = await userRepo.GetByEmailAsync(request.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return ServiceResponse<LoginDtoResponse>.UnauthorizedResponse("Invalid email or password");

        return ServiceResponse<LoginDtoResponse>.SuccessResponse(
            GenerateToken(user.UserId, user.UserName, user.Email));
    }

    public async Task<ServiceResponse<LoginDtoResponse>> RegisterAsync(RegisterDtoRequest request)
    {
        if (await userRepo.UserNameExistsAsync(request.UserName))
            return ServiceResponse<LoginDtoResponse>.ErrorResponse("Username already taken");

        if (await userRepo.EmailExistsAsync(request.Email))
            return ServiceResponse<LoginDtoResponse>.ErrorResponse("Email already in use");

        var user = await userRepo.RegisterUserAsync(request);

        return ServiceResponse<LoginDtoResponse>.SuccessResponse(
            GenerateToken(user.UserId, user.UserName, user.Email));
    }

    private LoginDtoResponse GenerateToken(int userId, string userName, string email)
    {
        var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Email, email),
        };

        var token = new JwtSecurityToken(
            issuer:             config["Jwt:Issuer"],
            audience:           config["Jwt:Audience"],
            claims:             claims,
            expires:            DateTime.UtcNow.AddMinutes(int.Parse(config["Jwt:ExpiresInMinutes"]!)),
            signingCredentials: creds
        );

        return new LoginDtoResponse
        {
            Token    = new JwtSecurityTokenHandler().WriteToken(token),
            UserName = userName,
            UserId   = userId
        };
    }
}
