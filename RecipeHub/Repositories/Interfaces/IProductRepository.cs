using RecipeHub.DTOs.ProductDTOs;
using RecipeHub.Models;

namespace RecipeHub.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<ProductDtoResponse>> GetAllProductsAsync();
    Task<ProductDtoResponse> CreateProductAsync(ProductDtoRequest dto);
    Task<ProductDtoResponse?> UpdateProductAsync(int id, ProductDtoRequest dto);
    Task<bool> DeleteProductAsync(int id);
    Task<bool> ValidateProductAsync(int id);
    Task<bool> ValidateIfProductExistAsync(string name);
    Task<HashSet<int>> GetExistingProductIdsAsync(IEnumerable<int> productIds);
}