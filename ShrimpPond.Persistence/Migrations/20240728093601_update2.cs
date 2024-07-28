using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Pond_PondId",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "UsedDate",
                table: "Medicine");

            migrationBuilder.AlterColumn<string>(
                name: "PondId",
                table: "Medicine",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "MedicineFeeding",
                columns: table => new
                {
                    MedicineFeedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PondId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineFeeding", x => x.MedicineFeedingId);
                });

            migrationBuilder.CreateTable(
                name: "MedicineForFeeding",
                columns: table => new
                {
                    MedicineForFeedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    MedicineFeedingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineForFeeding", x => x.MedicineForFeedingId);
                    table.ForeignKey(
                        name: "FK_MedicineForFeeding_MedicineFeeding_MedicineFeedingId",
                        column: x => x.MedicineFeedingId,
                        principalTable: "MedicineFeeding",
                        principalColumn: "MedicineFeedingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineForFeeding_MedicineFeedingId",
                table: "MedicineForFeeding",
                column: "MedicineFeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Pond_PondId",
                table: "Medicine",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Pond_PondId",
                table: "Medicine");

            migrationBuilder.DropTable(
                name: "MedicineForFeeding");

            migrationBuilder.DropTable(
                name: "MedicineFeeding");

            migrationBuilder.AlterColumn<string>(
                name: "PondId",
                table: "Medicine",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Amount",
                table: "Medicine",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "UsedDate",
                table: "Medicine",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Pond_PondId",
                table: "Medicine",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
