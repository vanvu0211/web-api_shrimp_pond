using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class updatefeeding2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeding_Pond_PondId",
                table: "Feeding");

            migrationBuilder.DropIndex(
                name: "IX_Feeding_PondId",
                table: "Feeding");

            migrationBuilder.AddColumn<string>(
                name: "PondId",
                table: "FeedingFood",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PondId",
                table: "Feeding",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PondId",
                table: "FeedingFood");

            migrationBuilder.AlterColumn<string>(
                name: "PondId",
                table: "Feeding",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Feeding_PondId",
                table: "Feeding",
                column: "PondId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feeding_Pond_PondId",
                table: "Feeding",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
