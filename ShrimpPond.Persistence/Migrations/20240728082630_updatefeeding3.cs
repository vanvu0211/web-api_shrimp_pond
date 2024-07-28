using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class updatefeeding3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedingFood_Feeding_FeedingId",
                table: "FeedingFood");

            migrationBuilder.DropIndex(
                name: "IX_FeedingFood_FeedingId",
                table: "FeedingFood");

            migrationBuilder.DropColumn(
                name: "PondId",
                table: "FeedingFood");

            migrationBuilder.AlterColumn<int>(
                name: "FeedingId",
                table: "FeedingFood",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FoodFeedingFeedingId",
                table: "FeedingFood",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedingFood_FoodFeedingFeedingId",
                table: "FeedingFood",
                column: "FoodFeedingFeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedingFood_Feeding_FoodFeedingFeedingId",
                table: "FeedingFood",
                column: "FoodFeedingFeedingId",
                principalTable: "Feeding",
                principalColumn: "FeedingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedingFood_Feeding_FoodFeedingFeedingId",
                table: "FeedingFood");

            migrationBuilder.DropIndex(
                name: "IX_FeedingFood_FoodFeedingFeedingId",
                table: "FeedingFood");

            migrationBuilder.DropColumn(
                name: "FoodFeedingFeedingId",
                table: "FeedingFood");

            migrationBuilder.AlterColumn<int>(
                name: "FeedingId",
                table: "FeedingFood",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PondId",
                table: "FeedingFood",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingFood_FeedingId",
                table: "FeedingFood",
                column: "FeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedingFood_Feeding_FeedingId",
                table: "FeedingFood",
                column: "FeedingId",
                principalTable: "Feeding",
                principalColumn: "FeedingId");
        }
    }
}
