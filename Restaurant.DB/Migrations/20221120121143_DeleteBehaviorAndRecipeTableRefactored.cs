using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.DB.Migrations
{
    public partial class DeleteBehaviorAndRecipeTableRefactored : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealsCategories_MealCategoryId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cities_CityId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersElements_Meals_MealId",
                table: "OrdersElements");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersElements_Orders_OrderId",
                table: "OrdersElements");

            migrationBuilder.DropTable(
                name: "Recipes");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealsCategories_MealCategoryId",
                table: "Meals",
                column: "MealCategoryId",
                principalTable: "MealsCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cities_CityId",
                table: "Orders",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersElements_Meals_MealId",
                table: "OrdersElements",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersElements_Orders_OrderId",
                table: "OrdersElements",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Meals_MealId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealsCategories_MealCategoryId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cities_CityId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersElements_Meals_MealId",
                table: "OrdersElements");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersElements_Orders_OrderId",
                table: "OrdersElements");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_MealId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    MealId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => new { x.MealId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_Recipes_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_IngredientId",
                table: "Recipes",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealsCategories_MealCategoryId",
                table: "Meals",
                column: "MealCategoryId",
                principalTable: "MealsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cities_CityId",
                table: "Orders",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersElements_Meals_MealId",
                table: "OrdersElements",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersElements_Orders_OrderId",
                table: "OrdersElements",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
