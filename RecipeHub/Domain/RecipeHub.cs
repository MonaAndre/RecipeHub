using RecipeHub.Common;
using RecipeHub.DTOs.CommentDTOs;
using RecipeHub.DTOs.ProductDTOs;
using RecipeHub.DTOs.RecipeDTOs;
using RecipeHub.Services.Interfaces;

namespace RecipeHub.Domain;

public class RecipeHub : IRecipeHub
{
    private readonly IProductService _productService;
    private readonly IRecipeService  _recipeService;
    private readonly ICommentService _commentService;

    public RecipeHub(IProductService productService, IRecipeService recipeService, ICommentService commentService)
    {
        _productService = productService;
        _recipeService  = recipeService;
        _commentService = commentService;
    }

    // Products
    public async Task<ServiceResponse<List<ProductDtoResponse>>> GetAllProductsAsync()
        => await _productService.GetAllProductsAsync();

    public async Task<ServiceResponse<ProductDtoResponse>> CreateProductAsync(ProductDtoRequest dto)
        => await _productService.CreateProductAsync(dto);

    public async Task<ServiceResponse<ProductDtoResponse>> UpdateProductAsync(int id, ProductDtoRequest dto)
        => await _productService.UpdateProductAsync(id, dto);

    public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        => await _productService.DeleteProductAsync(id);

    // Recipes
    public async Task<ServiceResponse<List<RecipeDtoResponse>>> GetAllRecipesAsync()
        => await _recipeService.GetAllRecipesAsync();

    public async Task<ServiceResponse<RecipesByPageDtoResponse>> GetRecipesAsync(RecipesByPageDtoRequest request)
        => await _recipeService.GetRecipesAsync(request);

    public async Task<ServiceResponse<RecipeDetailsDtoResponse>> GetRecipeByIdAsync(int id)
        => await _recipeService.GetByIdAsync(id);

    public async Task<ServiceResponse<RecipeDetailsDtoResponse>> CreateRecipeAsync(CreateRecipeDtoRequest dto, int userId)
        => await _recipeService.CreateRecipeAsync(dto, userId);

    public async Task<ServiceResponse<RecipeDetailsDtoResponse>> UpdateRecipeAsync(int id, UpdateRecipeDtoRequest dto, int userId)
        => await _recipeService.UpdateRecipeAsync(id, dto, userId);

    public async Task<ServiceResponse<bool>> DeleteRecipeAsync(int id, int userId)
        => await _recipeService.DeleteRecipeAsync(id, userId);

    // Comments
    public async Task<ServiceResponse<List<CommentDtoResponse>>> GetCommentsByRecipeIdAsync(int recipeId)
        => await _commentService.GetByRecipeIdAsync(recipeId);

    public async Task<ServiceResponse<CommentDtoResponse>> CreateCommentAsync(int recipeId, int userId, CreateCommentDtoRequest dto)
        => await _commentService.CreateAsync(recipeId, userId, dto);

    public async Task<ServiceResponse<CommentDtoResponse>> UpdateCommentAsync(int commentId, int userId, UpdateCommentDtoRequest dto)
        => await _commentService.UpdateAsync(commentId, userId, dto);

    public async Task<ServiceResponse<bool>> DeleteCommentAsync(int commentId, int userId)
        => await _commentService.DeleteAsync(commentId, userId);
}
