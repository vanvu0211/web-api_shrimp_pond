using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class abd1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSettingObject_TimeSettings_TimeSettingId",
                table: "TimeSettingObject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSettingObject",
                table: "TimeSettingObject");

            migrationBuilder.RenameTable(
                name: "TimeSettingObject",
                newName: "timeSettingObjects");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSettingObject_TimeSettingId",
                table: "timeSettingObjects",
                newName: "IX_timeSettingObjects_TimeSettingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_timeSettingObjects",
                table: "timeSettingObjects",
                column: "TimeSettingObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_timeSettingObjects_TimeSettings_TimeSettingId",
                table: "timeSettingObjects",
                column: "TimeSettingId",
                principalTable: "TimeSettings",
                principalColumn: "TimeSettingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timeSettingObjects_TimeSettings_TimeSettingId",
                table: "timeSettingObjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_timeSettingObjects",
                table: "timeSettingObjects");

            migrationBuilder.RenameTable(
                name: "timeSettingObjects",
                newName: "TimeSettingObject");

            migrationBuilder.RenameIndex(
                name: "IX_timeSettingObjects_TimeSettingId",
                table: "TimeSettingObject",
                newName: "IX_TimeSettingObject_TimeSettingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSettingObject",
                table: "TimeSettingObject",
                column: "TimeSettingObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSettingObject_TimeSettings_TimeSettingId",
                table: "TimeSettingObject",
                column: "TimeSettingId",
                principalTable: "TimeSettings",
                principalColumn: "TimeSettingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
