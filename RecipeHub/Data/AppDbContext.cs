using Microsoft.EntityFrameworkCore;
using RecipeHub.Models;

namespace RecipeHub.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
    public DbSet<InstructionStep> InstructionSteps => Set<InstructionStep>();
    public DbSet<Comment> Comments => Set<Comment>();

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
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(u => u.UserId);
            user.Property(u => u.UserId).ValueGeneratedOnAdd();
            user.Property(u=>u.PasswordHash).IsRequired();
            user.Property(u => u.Email).IsRequired();
            user.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            user.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            user.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            user.HasIndex(u => u.Email).IsUnique();
            user.HasIndex(u => u.UserName).IsUnique();
        });
        modelBuilder.Entity<Recipe>(recipe =>
        {
            recipe.HasKey(r => r.RecipeId);
            recipe.Property(r => r.RecipeId).ValueGeneratedOnAdd();
            recipe.Property(r => r.RecipeName).IsRequired().HasMaxLength(100);
            recipe.Property(r => r.RecipeDescription).HasMaxLength(1000);
            recipe.Property(r => r.RecipeCategory).HasMaxLength(100);
            recipe.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
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
        modelBuilder.Entity<Comment>(c =>
        {
            c.HasKey(x => x.CommentId);
            c.Property(x => x.CommentId).ValueGeneratedOnAdd();
            c.Property(x => x.Text).IsRequired().HasMaxLength(1000);
            c.HasOne(x => x.Recipe)
                .WithMany(r => r.Comments)
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            c.HasOne(x => x.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}