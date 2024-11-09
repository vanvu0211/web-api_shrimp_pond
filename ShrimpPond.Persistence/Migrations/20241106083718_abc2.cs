using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class abc2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeSettingId",
                table: "TimeSettingObject",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSettingObject_TimeSettingId",
                table: "TimeSettingObject",
                column: "TimeSettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSettingObject_TimeSettings_TimeSettingId",
                table: "TimeSettingObject",
                column: "TimeSettingId",
                principalTable: "TimeSettings",
                principalColumn: "TimeSettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSettingObject_TimeSettings_TimeSettingId",
                table: "TimeSettingObject");

            migrationBuilder.DropIndex(
                name: "IX_TimeSettingObject_TimeSettingId",
                table: "TimeSettingObject");

            migrationBuilder.DropColumn(
                name: "TimeSettingId",
                table: "TimeSettingObject");
        }
    }
}
