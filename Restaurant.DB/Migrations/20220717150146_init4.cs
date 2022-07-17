using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.DB.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersDetails_Cities_CityId",
                table: "UsersDetails");

            migrationBuilder.AlterColumn<short>(
                name: "CityId",
                table: "UsersDetails",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDetails_Cities_CityId",
                table: "UsersDetails",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersDetails_Cities_CityId",
                table: "UsersDetails");

            migrationBuilder.AlterColumn<short>(
                name: "CityId",
                table: "UsersDetails",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDetails_Cities_CityId",
                table: "UsersDetails",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
