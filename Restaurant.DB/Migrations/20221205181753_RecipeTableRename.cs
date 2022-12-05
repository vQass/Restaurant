using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.DB.Migrations
{
    public partial class RecipeTableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientMeal_Ingredients_IngredientsId",
                table: "IngredientMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientMeal_Meals_MealsId",
                table: "IngredientMeal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientMeal",
                table: "IngredientMeal");

            migrationBuilder.RenameTable(
                name: "IngredientMeal",
                newName: "Recipes");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientMeal_MealsId",
                table: "Recipes",
                newName: "IX_Recipes_MealsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                columns: new[] { "IngredientsId", "MealsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Ingredients_IngredientsId",
                table: "Recipes",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Meals_MealsId",
                table: "Recipes",
                column: "MealsId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Ingredients_IngredientsId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Meals_MealsId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "IngredientMeal");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_MealsId",
                table: "IngredientMeal",
                newName: "IX_IngredientMeal_MealsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientMeal",
                table: "IngredientMeal",
                columns: new[] { "IngredientsId", "MealsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientMeal_Ingredients_IngredientsId",
                table: "IngredientMeal",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientMeal_Meals_MealsId",
                table: "IngredientMeal",
                column: "MealsId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
