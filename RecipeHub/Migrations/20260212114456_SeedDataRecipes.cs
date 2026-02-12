using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // PRODUCTS
            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "product_id", "product_namn", "category" },
                values: new object[,]
                {
                    { 1, "Flour", "Baking" },
                    { 2, "Milk", "Dairy" },
                    { 3, "Egg", "Dairy" }
                });

            // RECIPES
            migrationBuilder.InsertData(
                table: "recipes",
                columns: new[] { "recipe_id", "recipe_name", "recipe_description", "recipe_category" },
                values: new object[]
                {
                    1, "Pancakes", "Classic fluffy pancakes", "Breakfast"
                });

            // INSTRUCTION STEPS
            migrationBuilder.InsertData(
                table: "instruction_steps",
                columns: new[] { "step_id", "step_text", "step_number", "recipe_id" },
                values: new object[,]
                {
                    { 1, "Mix all ingredients", 1, 1 },
                    { 2, "Heat pan", 2, 1 },
                    { 3, "Pour batter and cook", 3, 1 }
                });

            // RECIPE INGREDIENTS
            migrationBuilder.InsertData(
                table: "recipe_ingredients",
                columns: new[] { "recipe_id", "product_id", "quantity", "unit" },
                values: new object[,]
                {
                    { 1, 1, 2m, "dl" },
                    { 1, 2, 3m, "dl" },
                    { 1, 3, 2m, "pcs" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
