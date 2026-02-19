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

    public async Task<List<ProductDtoResponse>> GetAllProductsAsync()
    {
        return await _context.Products
            .Select(p => new ProductDtoResponse
            {
                Id = p.ProductId,
                Name = p.ProductNamn,
                Category = p.Category!
            })
            .ToListAsync();
    }

    public async Task<ProductDtoResponse?> GetProductAsync(int id)
    {
        var product = await _context.Products
            .Where(p => p.ProductId == id)
            .Select(p => new ProductDtoResponse
            {
                Id = p.ProductId,
                Name = p.ProductNamn,
                Category = p.Category!
            }).FirstOrDefaultAsync();
        return product;
    }

    public async Task<ProductDtoResponse> CreateProductAsync(ProductDtoRequest dto)
    {
        var productToCreate = new Product
        {
            ProductNamn = dto.Name,
            Category = dto.Category
        };
        await _context.Products.AddAsync(productToCreate);
        await _context.SaveChangesAsync();
        var newProduct = new ProductDtoResponse
        {
            Id = productToCreate.ProductId,
            Name = productToCreate.ProductNamn,
        };
        return newProduct;
    }

    public async Task<ProductDtoResponse?> UpdateProductAsync(int id, ProductDtoRequest dto)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        product!.ProductNamn = dto.Name;
        product.Category = dto.Category;
        await _context.SaveChangesAsync();
        var newProduct = await _context.Products
            .Where(p => p.ProductId == id)
            .Select(p => new ProductDtoResponse
            {
                Id = p.ProductId,
                Name = p.ProductNamn,
                Category = p.Category!
            })
            .FirstOrDefaultAsync();
        return newProduct;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var productToDelete = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        if (productToDelete is null) return false;
        _context.Products.Remove(productToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ValidateProductAsync(int id)
    {
        return await _context.Products.AnyAsync(p => p.ProductId == id);
    }

    public async Task<bool> ValidateIfProductExistAsync(string name)
    {
        return await _context.Products
            .AnyAsync(p =>
                EF.Functions.ILike(p.ProductNamn, name));
    }
}