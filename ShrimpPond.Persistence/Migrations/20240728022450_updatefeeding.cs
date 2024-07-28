using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class updatefeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Pond_PondId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "UsedDate",
                table: "Food");

            migrationBuilder.AlterColumn<string>(
                name: "PondId",
                table: "Food",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Feeding",
                columns: table => new
                {
                    FeedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeding", x => x.FeedingId);
                    table.ForeignKey(
                        name: "FK_Feeding_Pond_PondId",
                        column: x => x.PondId,
                        principalTable: "Pond",
                        principalColumn: "PondId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedingFood",
                columns: table => new
                {
                    FeedingFoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    FeedingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingFood", x => x.FeedingFoodId);
                    table.ForeignKey(
                        name: "FK_FeedingFood_Feeding_FeedingId",
                        column: x => x.FeedingId,
                        principalTable: "Feeding",
                        principalColumn: "FeedingId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feeding_PondId",
                table: "Feeding",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingFood_FeedingId",
                table: "FeedingFood",
                column: "FeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Pond_PondId",
                table: "Food",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Pond_PondId",
                table: "Food");

            migrationBuilder.DropTable(
                name: "FeedingFood");

            migrationBuilder.DropTable(
                name: "Feeding");

            migrationBuilder.AlterColumn<string>(
                name: "PondId",
                table: "Food",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Amount",
                table: "Food",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "UsedDate",
                table: "Food",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Pond_PondId",
                table: "Food",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
