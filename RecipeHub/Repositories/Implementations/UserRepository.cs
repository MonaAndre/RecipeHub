using Microsoft.EntityFrameworkCore;
using RecipeHub.Common;
using RecipeHub.Data;
using RecipeHub.DTOs;
using RecipeHub.DTOs.AuthDTOs;
using RecipeHub.Models;
using RecipeHub.Repositories.Interfaces;

namespace RecipeHub.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserInternalDto?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Where(u => u.Email == email)
            .Select(u => new UserInternalDto
            {
                UserId       = u.UserId,
                UserName     = u.UserName,
                Email        = u.Email,
                PasswordHash = u.PasswordHash,
            }).FirstOrDefaultAsync();
    }

    public async Task<UserDtoResponse?> GetByUserNameAsync(string userName)
    {
        var user = await _context.Users.Where(u => u.UserName == userName)
            .Select(u => new UserDtoResponse
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Birthdate = u.Birthdate,
                IsConfirmed = u.IsConfirmed,
                IsDeleted = u.IsDeleted,
                CreatedAt = u.CreatedAt,
            }).FirstOrDefaultAsync();
        return user;
    }

    public async Task<bool> UserNameExistsAsync(string userName)
    {
        return await _context.Users.AnyAsync(u => u.UserName == userName);
    }
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<UserDtoResponse> RegisterUserAsync(RegisterDtoRequest dto)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            UserName     = dto.UserName,
            Email        = dto.Email,
            PasswordHash = passwordHash,
            FirstName    = dto.FirstName,
            LastName     = dto.LastName,
            CreatedAt    = DateTime.UtcNow,
            IsConfirmed  = false,
            IsDeleted    = false
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDtoResponse
        {
            UserId      = user.UserId,
            UserName    = user.UserName,
            Email       = user.Email,
            FirstName   = user.FirstName,
            LastName    = user.LastName,
            Birthdate   = user.Birthdate,
            IsConfirmed = user.IsConfirmed,
            IsDeleted   = user.IsDeleted,
            CreatedAt   = user.CreatedAt
        };
    }

    

}