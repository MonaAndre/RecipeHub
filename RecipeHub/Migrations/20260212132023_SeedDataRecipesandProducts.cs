using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataRecipesandProducts : Migration
    {
        /// <inheritdoc />
       protected override void Up(MigrationBuilder migrationBuilder)
{
    // --------------------
    // PRODUCTS (new)
    // --------------------
    migrationBuilder.InsertData(
        table: "products",
        columns: new[] { "product_id", "product_namn", "category" },
        values: new object[,]
        {
            { 4, "Pasta", "Grains" },
            { 5, "Tomato Sauce", "Vegetables" },
            { 6, "Parmesan", "Dairy" },
            { 7, "Lettuce", "Vegetables" },
            { 8, "Cucumber", "Vegetables" },
            { 9, "Olive Oil", "Pantry" },
            { 10, "Salt", "Pantry" }
        });

    // --------------------
    // RECIPES (new)
    // --------------------
    migrationBuilder.InsertData(
        table: "recipes",
        columns: new[] { "recipe_id", "recipe_name", "recipe_description", "recipe_category" },
        values: new object[,]
        {
            { 2, "Tomato Pasta", "Quick pasta with tomato sauce and parmesan", "Dinner" },
            { 3, "Fresh Salad", "Simple salad with cucumber and olive oil", "Lunch" },
            { 4, "Parmesan Pasta", "Pasta with olive oil and parmesan", "Dinner" }
        });

    // --------------------
    // INSTRUCTION STEPS (new)
    // --------------------
    migrationBuilder.InsertData(
        table: "instruction_steps",
        columns: new[] { "step_id", "step_text", "step_number", "recipe_id" },
        values: new object[,]
        {
            // Recipe 2: Tomato Pasta
            { 4, "Boil pasta in salted water", 1, 2 },
            { 5, "Warm tomato sauce in a pan", 2, 2 },
            { 6, "Mix pasta with sauce and top with parmesan", 3, 2 },

            // Recipe 3: Fresh Salad
            { 7, "Chop lettuce and cucumber", 1, 3 },
            { 8, "Add olive oil and salt", 2, 3 },
            { 9, "Mix and serve", 3, 3 },

            // Recipe 4: Parmesan Pasta
            { 10, "Boil pasta in salted water", 1, 4 },
            { 11, "Drain and add olive oil", 2, 4 },
            { 12, "Top with parmesan and serve", 3, 4 }
        });

    // --------------------
    // RECIPE INGREDIENTS (new)
    // --------------------
    migrationBuilder.InsertData(
        table: "recipe_ingredients",
        columns: new[] { "recipe_id", "product_id", "quantity", "unit" },
        values: new object[,]
        {
            // Recipe 2
            { 2, 4, 200m, "g" },
            { 2, 5, 150m, "g" },
            { 2, 6, 30m, "g" },
            { 2, 10, 1m, "tsp" },

            // Recipe 3
            { 3, 7, 1m, "head" },
            { 3, 8, 1m, "pcs" },
            { 3, 9, 1m, "tbsp" },
            { 3, 10, 1m, "pinch" },

            // Recipe 4
            { 4, 4, 200m, "g" },
            { 4, 6, 40m, "g" },
            { 4, 9, 1m, "tbsp" },
            { 4, 10, 1m, "tsp" }
        });
}


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
