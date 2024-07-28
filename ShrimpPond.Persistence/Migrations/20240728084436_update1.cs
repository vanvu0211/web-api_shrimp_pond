using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedingFood_Feeding_FoodFeedingId",
                table: "FeedingFood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedingFood",
                table: "FeedingFood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feeding",
                table: "Feeding");

            migrationBuilder.RenameTable(
                name: "FeedingFood",
                newName: "FoodForFeeding");

            migrationBuilder.RenameTable(
                name: "Feeding",
                newName: "FoodFeeding");

            migrationBuilder.RenameIndex(
                name: "IX_FeedingFood_FoodFeedingId",
                table: "FoodForFeeding",
                newName: "IX_FoodForFeeding_FoodFeedingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodForFeeding",
                table: "FoodForFeeding",
                column: "FoodForFeedingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodFeeding",
                table: "FoodFeeding",
                column: "FoodFeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodForFeeding_FoodFeeding_FoodFeedingId",
                table: "FoodForFeeding",
                column: "FoodFeedingId",
                principalTable: "FoodFeeding",
                principalColumn: "FoodFeedingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodForFeeding_FoodFeeding_FoodFeedingId",
                table: "FoodForFeeding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodForFeeding",
                table: "FoodForFeeding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodFeeding",
                table: "FoodFeeding");

            migrationBuilder.RenameTable(
                name: "FoodForFeeding",
                newName: "FeedingFood");

            migrationBuilder.RenameTable(
                name: "FoodFeeding",
                newName: "Feeding");

            migrationBuilder.RenameIndex(
                name: "IX_FoodForFeeding_FoodFeedingId",
                table: "FeedingFood",
                newName: "IX_FeedingFood_FoodFeedingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedingFood",
                table: "FeedingFood",
                column: "FoodForFeedingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feeding",
                table: "Feeding",
                column: "FoodFeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedingFood_Feeding_FoodFeedingId",
                table: "FeedingFood",
                column: "FoodFeedingId",
                principalTable: "Feeding",
                principalColumn: "FoodFeedingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
