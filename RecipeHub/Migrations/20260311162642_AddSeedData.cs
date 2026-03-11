using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Users
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_name", "password_hash", "email", "first_name", "last_name", "birthdate", "is_confirmed", "is_deleted", "created_at" },
                values: new object[,]
                {
                    { "chef_anna",   "$2a$11$dummyhashforannaXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", "anna@recipehub.com",  "Anna",  "Johnson", new DateTime(1990, 5, 12, 0, 0, 0, DateTimeKind.Utc), true, false, DateTime.UtcNow },
                    { "foodie_erik", "$2a$11$dummyhashforerikXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", "erik@recipehub.com",  "Erik",  "Williams", new DateTime(1985, 8, 23, 0, 0, 0, DateTimeKind.Utc), true, false, DateTime.UtcNow },
                    { "bake_sofia",  "$2a$11$dummyhashforsofiaXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", "sofia@recipehub.com", "Sofia", "Martinez", new DateTime(1995, 2,  3, 0, 0, 0, DateTimeKind.Utc), true, false, DateTime.UtcNow },
                }
            );

            // Products (ingredients)
            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "product_namn", "category" },
                values: new object[,]
                {
                    { "Flour",          "Dry Goods"  },
                    { "Sugar",          "Dry Goods"  },
                    { "Eggs",           "Dairy"      },
                    { "Butter",         "Dairy"      },
                    { "Milk",           "Dairy"      },
                    { "Salt",           "Spices"     },
                    { "Black Pepper",   "Spices"     },
                    { "Olive Oil",      "Oils"       },
                    { "Garlic",         "Vegetables" },
                    { "Onion",          "Vegetables" },
                    { "Tomato",         "Vegetables" },
                    { "Pasta",          "Dry Goods"  },
                    { "Chicken Breast", "Meat"       },
                    { "Salmon",         "Fish"       },
                    { "Potato",         "Vegetables" },
                }
            );

            // Recipes (user_id 1 = Anna, 2 = Erik, 3 = Sofia)
            migrationBuilder.InsertData(
                table: "recipes",
                columns: new[] { "recipe_name", "recipe_description", "recipe_category", "user_id" },
                values: new object[,]
                {
                    { "Pancakes",           "Classic thin pancakes, light and delicious.",               "Breakfast", 1 },
                    { "Pasta with Tomato Sauce", "Simple and quick pasta with homemade tomato sauce.",   "Dinner",    2 },
                    { "Oven-Baked Salmon",  "Juicy salmon baked in the oven with lemon and herbs.",      "Dinner",    1 },
                    { "Chicken Stew",       "Creamy chicken stew with garlic and potatoes.",             "Dinner",    3 },
                    { "Sponge Cake",        "Fluffy classic sponge cake, perfect for afternoon tea.",    "Baking",    3 },
                }
            );

            // RecipeIngredients
            // Pancakes (recipe_id=1): Flour(1), Eggs(3), Milk(5), Butter(4), Salt(6)
            migrationBuilder.InsertData(
                table: "recipe_ingredients",
                columns: new[] { "recipe_id", "product_id", "quantity", "unit" },
                values: new object[,]
                {
                    { 1, 1,  3m,    "cups"  },
                    { 1, 3,  3m,    "pcs"   },
                    { 1, 5,  6m,    "cups"  },
                    { 1, 4,  2m,    "tbsp"  },
                    { 1, 6,  0.5m,  "tsp"   },
                }
            );

            // Pasta with Tomato Sauce (recipe_id=2): Pasta(12), Tomato(11), Garlic(9), Onion(10), Olive Oil(8), Salt(6), Black Pepper(7)
            migrationBuilder.InsertData(
                table: "recipe_ingredients",
                columns: new[] { "recipe_id", "product_id", "quantity", "unit" },
                values: new object[,]
                {
                    { 2, 12, 400m, "g"    },
                    { 2, 11, 4m,   "pcs"  },
                    { 2, 9,  3m,   "cloves" },
                    { 2, 10, 1m,   "pcs"  },
                    { 2, 8,  2m,   "tbsp" },
                    { 2, 6,  1m,   "tsp"  },
                    { 2, 7,  0.5m, "tsp"  },
                }
            );

            // Oven-Baked Salmon (recipe_id=3): Salmon(14), Olive Oil(8), Salt(6), Black Pepper(7)
            migrationBuilder.InsertData(
                table: "recipe_ingredients",
                columns: new[] { "recipe_id", "product_id", "quantity", "unit" },
                values: new object[,]
                {
                    { 3, 14, 600m, "g"    },
                    { 3, 8,  2m,   "tbsp" },
                    { 3, 6,  1m,   "tsp"  },
                    { 3, 7,  0.5m, "tsp"  },
                }
            );

            // Chicken Stew (recipe_id=4): Chicken Breast(13), Potato(15), Garlic(9), Onion(10), Butter(4), Salt(6)
            migrationBuilder.InsertData(
                table: "recipe_ingredients",
                columns: new[] { "recipe_id", "product_id", "quantity", "unit" },
                values: new object[,]
                {
                    { 4, 13, 500m, "g"      },
                    { 4, 15, 4m,   "pcs"    },
                    { 4, 9,  2m,   "cloves" },
                    { 4, 10, 1m,   "pcs"    },
                    { 4, 4,  2m,   "tbsp"   },
                    { 4, 6,  1m,   "tsp"    },
                }
            );

            // Sponge Cake (recipe_id=5): Flour(1), Sugar(2), Eggs(3), Butter(4), Milk(5), Salt(6)
            migrationBuilder.InsertData(
                table: "recipe_ingredients",
                columns: new[] { "recipe_id", "product_id", "quantity", "unit" },
                values: new object[,]
                {
                    { 5, 1, 3m,   "cups" },
                    { 5, 2, 2m,   "cups" },
                    { 5, 3, 3m,   "pcs"  },
                    { 5, 4, 100m, "g"    },
                    { 5, 5, 1m,   "cup"  },
                    { 5, 6, 0.5m, "tsp"  },
                }
            );

            // InstructionSteps
            // Pancakes
            migrationBuilder.InsertData(
                table: "instruction_steps",
                columns: new[] { "recipe_id", "step_number", "step_text" },
                values: new object[,]
                {
                    { 1, 1, "Whisk together flour, eggs and half the milk until you have a smooth batter." },
                    { 1, 2, "Add the remaining milk and salt, stir well." },
                    { 1, 3, "Melt butter in a frying pan over medium heat." },
                    { 1, 4, "Cook thin pancakes until golden brown on both sides." },
                }
            );

            // Pasta with Tomato Sauce
            migrationBuilder.InsertData(
                table: "instruction_steps",
                columns: new[] { "recipe_id", "step_number", "step_text" },
                values: new object[,]
                {
                    { 2, 1, "Cook pasta according to package instructions in salted water." },
                    { 2, 2, "Finely chop onion and garlic, sauté in olive oil until soft." },
                    { 2, 3, "Add chopped tomatoes, salt and pepper. Simmer for 15 minutes." },
                    { 2, 4, "Toss the cooked pasta with the tomato sauce and serve." },
                }
            );

            // Oven-Baked Salmon
            migrationBuilder.InsertData(
                table: "instruction_steps",
                columns: new[] { "recipe_id", "step_number", "step_text" },
                values: new object[,]
                {
                    { 3, 1, "Preheat the oven to 200°C (400°F)." },
                    { 3, 2, "Place the salmon in a baking dish and brush with olive oil." },
                    { 3, 3, "Season with salt and pepper." },
                    { 3, 4, "Bake for 15–20 minutes until the salmon is cooked through." },
                }
            );

            // Chicken Stew
            migrationBuilder.InsertData(
                table: "instruction_steps",
                columns: new[] { "recipe_id", "step_number", "step_text" },
                values: new object[,]
                {
                    { 4, 1, "Cut the chicken into pieces and brown in butter in a pot." },
                    { 4, 2, "Add chopped onion and garlic, fry for 2 minutes." },
                    { 4, 3, "Peel and dice the potatoes, add to the pot." },
                    { 4, 4, "Pour in enough water to cover, season with salt. Simmer for 25 minutes." },
                }
            );

            // Sponge Cake
            migrationBuilder.InsertData(
                table: "instruction_steps",
                columns: new[] { "recipe_id", "step_number", "step_text" },
                values: new object[,]
                {
                    { 5, 1, "Preheat oven to 175°C (350°F). Grease and flour a cake pan." },
                    { 5, 2, "Beat eggs and sugar until light and fluffy." },
                    { 5, 3, "Melt the butter, mix in the milk, and fold into the egg mixture." },
                    { 5, 4, "Gently fold in flour and salt." },
                    { 5, 5, "Bake for 35–40 minutes until a skewer comes out clean." },
                }
            );

            // Comments
            migrationBuilder.InsertData(
                table: "comments",
                columns: new[] { "text", "created_at", "recipe_id", "user_id" },
                values: new object[,]
                {
                    { "Amazing recipe! The whole family loved the pancakes.",        DateTime.UtcNow, 1, 2 },
                    { "I added a little vanilla sugar — turned out even better!",    DateTime.UtcNow, 1, 3 },
                    { "Simple and tasty sauce, perfect for a weekday dinner.",       DateTime.UtcNow, 2, 1 },
                    { "Added some basil at the end, highly recommended!",            DateTime.UtcNow, 2, 3 },
                    { "The salmon turned out perfectly juicy, will make it again!",  DateTime.UtcNow, 3, 2 },
                    { "Really good stew, the potatoes absorbed all the flavor.",     DateTime.UtcNow, 4, 1 },
                    { "Best sponge cake I have tried, fluffy and just right.",       DateTime.UtcNow, 5, 2 },
                    { "Added a bit of cardamom to the batter, super delicious!",     DateTime.UtcNow, 5, 1 },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "comments",           keyColumn: "comment_id",  keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            migrationBuilder.DeleteData(table: "instruction_steps",  keyColumn: "step_id",     keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 });
            migrationBuilder.DeleteData(table: "recipe_ingredients", keyColumns: new[] { "recipe_id", "product_id" }, keyValues: new object[] { 1, 1 });
            migrationBuilder.DeleteData(table: "recipes",            keyColumn: "recipe_id",   keyValues: new object[] { 1, 2, 3, 4, 5 });
            migrationBuilder.DeleteData(table: "products",           keyColumn: "product_id",  keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
            migrationBuilder.DeleteData(table: "users",              keyColumn: "user_id",     keyValues: new object[] { 1, 2, 3 });
        }
    }
}
