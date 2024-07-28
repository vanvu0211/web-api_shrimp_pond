using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class UdQueriesSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "SizeShrimp",
                newName: "SizeValue");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "LossShrimp",
                newName: "LossValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SizeValue",
                table: "SizeShrimp",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "LossValue",
                table: "LossShrimp",
                newName: "Value");
        }
    }
}
