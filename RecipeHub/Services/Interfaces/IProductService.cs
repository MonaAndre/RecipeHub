using RecipeHub.DTOs.ProductDTOs;

namespace RecipeHub.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductDtoResponse>> GetAllProductsAsync();
    Task<ProductDtoResponse?> GetProductAsync(int id);
    Task<ProductDtoResponse> CreateProductAsync(ProductDtoRequest dto);
    Task<ProductDtoResponse?> UpdateProductAsync(int id, ProductDtoRequest dto);
    Task<bool> DeleteProductAsync(int id);
}