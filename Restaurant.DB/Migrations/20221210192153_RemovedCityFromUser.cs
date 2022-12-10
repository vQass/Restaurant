using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.DB.Migrations
{
    public partial class RemovedCityFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "CityId",
                table: "Users",
                type: "smallint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
