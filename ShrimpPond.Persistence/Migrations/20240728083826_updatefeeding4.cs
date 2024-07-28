using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class updatefeeding4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "FeedingId",
                table: "FeedingFood",
                newName: "FoodFeedingId");

            migrationBuilder.RenameColumn(
                name: "FeedingFoodId",
                table: "FeedingFood",
                newName: "FoodForFeedingId");

            migrationBuilder.RenameColumn(
                name: "FeedingId",
                table: "Feeding",
                newName: "FoodFeedingId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingFood_FoodFeedingId",
                table: "FeedingFood",
                column: "FoodFeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedingFood_Feeding_FoodFeedingId",
                table: "FeedingFood",
                column: "FoodFeedingId",
                principalTable: "Feeding",
                principalColumn: "FoodFeedingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedingFood_Feeding_FoodFeedingId",
                table: "FeedingFood");

            migrationBuilder.DropIndex(
                name: "IX_FeedingFood_FoodFeedingId",
                table: "FeedingFood");

            migrationBuilder.RenameColumn(
                name: "FoodFeedingId",
                table: "FeedingFood",
                newName: "FeedingId");

            migrationBuilder.RenameColumn(
                name: "FoodForFeedingId",
                table: "FeedingFood",
                newName: "FeedingFoodId");

            migrationBuilder.RenameColumn(
                name: "FoodFeedingId",
                table: "Feeding",
                newName: "FeedingId");

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
    }
}
