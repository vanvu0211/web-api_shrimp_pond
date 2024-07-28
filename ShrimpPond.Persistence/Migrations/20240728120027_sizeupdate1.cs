using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class sizeupdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Pond_PondId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_LossShrimp_Pond_PondId",
                table: "LossShrimp");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Pond_PondId",
                table: "Medicine");

            migrationBuilder.DropForeignKey(
                name: "FK_SizeShrimp_Pond_PondId",
                table: "SizeShrimp");

            migrationBuilder.DropIndex(
                name: "IX_SizeShrimp_PondId",
                table: "SizeShrimp");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_PondId",
                table: "Medicine");

            migrationBuilder.DropIndex(
                name: "IX_LossShrimp_PondId",
                table: "LossShrimp");

            migrationBuilder.DropIndex(
                name: "IX_Food_PondId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Collect_PondId",
                table: "Collect");

            migrationBuilder.DropColumn(
                name: "PondId",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "PondId",
                table: "Food");

            migrationBuilder.AlterColumn<string>(
                name: "PondId",
                table: "SizeShrimp",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PondId",
                table: "LossShrimp",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Collect_PondId",
                table: "Collect",
                column: "PondId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Collect_PondId",
                table: "Collect");

            migrationBuilder.AlterColumn<string>(
                name: "PondId",
                table: "SizeShrimp",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PondId",
                table: "Medicine",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PondId",
                table: "LossShrimp",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PondId",
                table: "Food",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SizeShrimp_PondId",
                table: "SizeShrimp",
                column: "PondId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_PondId",
                table: "Medicine",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_LossShrimp_PondId",
                table: "LossShrimp",
                column: "PondId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_PondId",
                table: "Food",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_Collect_PondId",
                table: "Collect",
                column: "PondId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Pond_PondId",
                table: "Food",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId");

            migrationBuilder.AddForeignKey(
                name: "FK_LossShrimp_Pond_PondId",
                table: "LossShrimp",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Pond_PondId",
                table: "Medicine",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId");

            migrationBuilder.AddForeignKey(
                name: "FK_SizeShrimp_Pond_PondId",
                table: "SizeShrimp",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
