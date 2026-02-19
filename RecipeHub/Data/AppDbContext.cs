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
    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
    public DbSet<InstructionStep> InstructionSteps => Set<InstructionStep>();

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
            recipe.Property(r => r.RecipeName).IsRequired().HasMaxLength(100);
            recipe.Property(r => r.RecipeDescription).HasMaxLength(1000);
            recipe.Property(r => r.RecipeCategory).HasMaxLength(100);
        });
        modelBuilder.Entity<RecipeIngredient>(recipeIngredient =>
        {
            recipeIngredient.HasKey(ri => new { ri.RecipeId, ri.ProductId });
        });
        modelBuilder.Entity<InstructionStep>(step =>
        {
            step.HasKey(s => s.StepId);
            step.Property(s => s.StepId).ValueGeneratedOnAdd();
            step.Property(s => s.StepText).IsRequired().HasMaxLength(400);
            step.HasIndex(s => new { s.RecipeId, s.StepNumber }).IsUnique();
            step.HasOne(s => s.Recipe)
                .WithMany(r => r.InstructionSteps)
                .HasForeignKey(s => s.RecipeId);
        });
    }
}