using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Harvest_HarvestId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Harvest_Pond_PondId",
                table: "Harvest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Harvest",
                table: "Harvest");

            migrationBuilder.RenameTable(
                name: "Harvest",
                newName: "Harvests");

            migrationBuilder.RenameIndex(
                name: "IX_Harvest_PondId",
                table: "Harvests",
                newName: "IX_Harvests_PondId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Harvests",
                table: "Harvests",
                column: "HarvestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Harvests_HarvestId",
                table: "Certificate",
                column: "HarvestId",
                principalTable: "Harvests",
                principalColumn: "HarvestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Harvests_Pond_PondId",
                table: "Harvests",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Harvests_HarvestId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Harvests_Pond_PondId",
                table: "Harvests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Harvests",
                table: "Harvests");

            migrationBuilder.RenameTable(
                name: "Harvests",
                newName: "Harvest");

            migrationBuilder.RenameIndex(
                name: "IX_Harvests_PondId",
                table: "Harvest",
                newName: "IX_Harvest_PondId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Harvest",
                table: "Harvest",
                column: "HarvestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Harvest_HarvestId",
                table: "Certificate",
                column: "HarvestId",
                principalTable: "Harvest",
                principalColumn: "HarvestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Harvest_Pond_PondId",
                table: "Harvest",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
