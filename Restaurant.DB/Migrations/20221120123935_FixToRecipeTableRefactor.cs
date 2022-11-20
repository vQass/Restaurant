using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.DB.Migrations
{
    public partial class FixToRecipeTableRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Meals_MealId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_MealId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "IngredientMeal",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    MealsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientMeal", x => new { x.IngredientsId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_IngredientMeal_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientMeal_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientMeal_MealsId",
                table: "IngredientMeal",
                column: "MealsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientMeal");

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_MealId",
                table: "Ingredients",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Meals_MealId",
                table: "Ingredients",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id");
        }
    }
}
