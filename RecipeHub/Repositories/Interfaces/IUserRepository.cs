using RecipeHub.Common;
using RecipeHub.DTOs;
using RecipeHub.DTOs.AuthDTOs;
using RecipeHub.Models;

namespace RecipeHub.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserInternalDto?> GetByEmailAsync(string email);
    Task<UserDtoResponse?> GetByUserNameAsync(string userName);
    Task<bool> UserNameExistsAsync(string userName);

    Task<bool> EmailExistsAsync(string email);
    Task<UserDtoResponse> RegisterUserAsync(RegisterDtoRequest dto);
}