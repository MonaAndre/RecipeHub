using Microsoft.EntityFrameworkCore;
using RecipeHub.Data;
using RecipeHub.DTOs.ProductDTOs;
using RecipeHub.Models;
using RecipeHub.Repositories.Interfaces;

namespace RecipeHub.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext constex)
    {
        _context = constex;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        return product;
    }

    public async Task<bool> CreateProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Product> UpdateProductAsync(int id, ProductDtoRequest dto)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        product!.ProductNamn = dto.Name;
        product.Category = dto.Category;
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var productToDelete = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        if (productToDelete is null) return false;
        _context.Products.Remove(productToDelete);
        return await _context.SaveChangesAsync() > 0;
    }
}