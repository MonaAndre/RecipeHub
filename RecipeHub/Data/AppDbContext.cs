using Microsoft.EntityFrameworkCore;
using RecipeHub.Models;

namespace RecipeHub.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Recipe> Recipes => Set<Recipe>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(product =>
        {
            product.HasKey(p => p.ProductId);
            product.Property(p => p.ProductId).ValueGeneratedOnAdd();
            product.Property(p => p.ProductNamn).IsRequired().HasMaxLength(100);
            product.HasIndex(p => p.ProductNamn).IsUnique();
            product.Property(p => p.Category).HasMaxLength(100);
        });
        modelBuilder.Entity<Recipe>(recipe =>
        {
            recipe.HasKey(r => r.RecipeId);
            recipe.Property(r => r.RecipeId).ValueGeneratedOnAdd();
            recipe.Property(r=>r.RecipeName).IsRequired().HasMaxLength(100); 
            recipe.Property(r => r.RecipeDescription).IsRequired().HasMaxLength(1000);
            recipe.Property(r => r.RecipeCategory).HasMaxLength(100);
        });
    }
}