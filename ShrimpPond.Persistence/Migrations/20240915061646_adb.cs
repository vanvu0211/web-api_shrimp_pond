using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class adb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FarmId",
                table: "PondType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FarmName",
                table: "PondType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PondType_FarmId",
                table: "PondType",
                column: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_PondType_Farms_FarmId",
                table: "PondType",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "FarmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PondType_Farms_FarmId",
                table: "PondType");

            migrationBuilder.DropIndex(
                name: "IX_PondType_FarmId",
                table: "PondType");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "PondType");

            migrationBuilder.DropColumn(
                name: "FarmName",
                table: "PondType");
        }
    }
}
