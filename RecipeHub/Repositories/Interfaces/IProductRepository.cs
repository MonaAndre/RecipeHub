using RecipeHub.DTOs.ProductDTOs;
using RecipeHub.Models;

namespace RecipeHub.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductAsync(int id);
    Task<bool> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(int id, ProductDtoRequest dto);
    Task<bool> DeleteProductAsync(int id);
}