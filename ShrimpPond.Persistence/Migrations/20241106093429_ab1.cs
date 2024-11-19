using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class ab1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSettingObject_TimeSettings_TimeSettingId",
                table: "TimeSettingObject");

            migrationBuilder.AlterColumn<int>(
                name: "TimeSettingId",
                table: "TimeSettingObject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSettingObject_TimeSettings_TimeSettingId",
                table: "TimeSettingObject",
                column: "TimeSettingId",
                principalTable: "TimeSettings",
                principalColumn: "TimeSettingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSettingObject_TimeSettings_TimeSettingId",
                table: "TimeSettingObject");

            migrationBuilder.AlterColumn<int>(
                name: "TimeSettingId",
                table: "TimeSettingObject",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSettingObject_TimeSettings_TimeSettingId",
                table: "TimeSettingObject",
                column: "TimeSettingId",
                principalTable: "TimeSettings",
                principalColumn: "TimeSettingId");
        }
    }
}
