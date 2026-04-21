using FluentAssertions;
using Moq;
using RecipeHub.Common;
using RecipeHub.DTOs.RecipeDTOs;
using RecipeHub.Repositories.Interfaces;
using Xunit;
using RecipeHub.Services.Implemintations;

namespace RecipeHub.Tests.Services;

public class RecipeServiceTests
{
    private readonly Mock<IRecipeRepository> _recipeRepoMock = new();
    private readonly Mock<IProductRepository> _productRepoMock = new();
    private RecipeService CreateService() => new(_recipeRepoMock.Object, _productRepoMock.Object);

    [Fact]
    public async Task GetAllRecipesAsync_ReturnsRecipes()
    {
        var recipes = new List<RecipeDtoResponse>
        {
            new() { Id = 1, RecipeName = "Pasta", RecipeCategory = "Dinner", UserName = "Alice" },
            new() { Id = 2, RecipeName = "Salad", RecipeCategory = "Lunch",  UserName = "Bob"   },
        };
        _recipeRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(recipes);

        var result = await CreateService().GetAllRecipesAsync();

        result.StatusCode.Should().Be(200);
        result.Data.Should().HaveCount(2);
        result.Data![0].RecipeName.Should().Be("Pasta");
    }

    [Fact]
    public async Task GetRecipesAsync_InvalidPage_ReturnsBadRequest()
    {
        var request = new RecipesByPageDtoRequest { Page = 0, PageSize = 10 };

        var result = await CreateService().GetRecipesAsync(request);

        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task GetRecipesAsync_InvalidPageSize_ReturnsBadRequest()
    {
        var request = new RecipesByPageDtoRequest { Page = 1, PageSize = 200 };

        var result = await CreateService().GetRecipesAsync(request);

        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task CreateRecipeAsync_EmptyName_ReturnsBadRequest()
    {
        var dto = new CreateRecipeDtoRequest { RecipeName = "   " };

        var result = await CreateService().CreateRecipeAsync(dto, userId: 1);

        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task DeleteRecipeAsync_InvalidId_ReturnsBadRequest()
    {
        var result = await CreateService().DeleteRecipeAsync(id: 0, userId: 1);

        result.StatusCode.Should().Be(400);
    }
}
