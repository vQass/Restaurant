using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.DB.Migrations
{
    public partial class RemovedPromotionIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromotionCodeId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PromotionCodeId",
                table: "Orders",
                type: "bigint",
                nullable: true);
        }
    }
}
