using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PHValue",
                columns: table => new
                {
                    PhId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHValue", x => x.PhId);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureValue",
                columns: table => new
                {
                    TemperatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureValue", x => x.TemperatureId);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentPara",
                columns: table => new
                {
                    EnvironmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PHValuePhId = table.Column<int>(type: "int", nullable: true),
                    TemperatureValueTemperatureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentPara", x => x.EnvironmentId);
                    table.ForeignKey(
                        name: "FK_EnvironmentPara_PHValue_PHValuePhId",
                        column: x => x.PHValuePhId,
                        principalTable: "PHValue",
                        principalColumn: "PhId");
                    table.ForeignKey(
                        name: "FK_EnvironmentPara_TemperatureValue_TemperatureValueTemperatureId",
                        column: x => x.TemperatureValueTemperatureId,
                        principalTable: "TemperatureValue",
                        principalColumn: "TemperatureId");
                });

            migrationBuilder.CreateTable(
                name: "NurseryPond",
                columns: table => new
                {
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PondHeight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PondRadius = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SeedId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShrimpCertificate = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: true),
                    ShrimpAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShrimpSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnvironmentsEnvironmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NurseryPond", x => x.PondId);
                    table.ForeignKey(
                        name: "FK_NurseryPond_EnvironmentPara_EnvironmentsEnvironmentId",
                        column: x => x.EnvironmentsEnvironmentId,
                        principalTable: "EnvironmentPara",
                        principalColumn: "EnvironmentId");
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    UsedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NurseryPondPondId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.FoodId);
                    table.ForeignKey(
                        name: "FK_Food_NurseryPond_NurseryPondPondId",
                        column: x => x.NurseryPondPondId,
                        principalTable: "NurseryPond",
                        principalColumn: "PondId");
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    UsedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NurseryPondPondId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.MedicineId);
                    table.ForeignKey(
                        name: "FK_Medicine_NurseryPond_NurseryPondPondId",
                        column: x => x.NurseryPondPondId,
                        principalTable: "NurseryPond",
                        principalColumn: "PondId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentPara_PHValuePhId",
                table: "EnvironmentPara",
                column: "PHValuePhId");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentPara_TemperatureValueTemperatureId",
                table: "EnvironmentPara",
                column: "TemperatureValueTemperatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_NurseryPondPondId",
                table: "Food",
                column: "NurseryPondPondId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_NurseryPondPondId",
                table: "Medicine",
                column: "NurseryPondPondId");

            migrationBuilder.CreateIndex(
                name: "IX_NurseryPond_EnvironmentsEnvironmentId",
                table: "NurseryPond",
                column: "EnvironmentsEnvironmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "NurseryPond");

            migrationBuilder.DropTable(
                name: "EnvironmentPara");

            migrationBuilder.DropTable(
                name: "PHValue");

            migrationBuilder.DropTable(
                name: "TemperatureValue");
        }
    }
}
