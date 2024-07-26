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
                name: "PondType",
                columns: table => new
                {
                    PondTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PondTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PondType", x => x.PondTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Pond",
                columns: table => new
                {
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Deep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PondTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PondTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OriginPondId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeedId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountShrimp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pond", x => x.PondId);
                    table.ForeignKey(
                        name: "FK_Pond_PondType_PondTypeId",
                        column: x => x.PondTypeId,
                        principalTable: "PondType",
                        principalColumn: "PondTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Collect",
                columns: table => new
                {
                    CollectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectTime = table.Column<int>(type: "int", nullable: false),
                    CollectType = table.Column<int>(type: "int", nullable: false),
                    SizeShrimpCollect = table.Column<float>(type: "real", nullable: false),
                    AmountShrimpCollect = table.Column<float>(type: "real", nullable: false),
                    CollectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collect", x => x.CollectId);
                    table.ForeignKey(
                        name: "FK_Collect_Pond_PondId",
                        column: x => x.PondId,
                        principalTable: "Pond",
                        principalColumn: "PondId",
                        onDelete: ReferentialAction.Cascade);
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
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.FoodId);
                    table.ForeignKey(
                        name: "FK_Food_Pond_PondId",
                        column: x => x.PondId,
                        principalTable: "Pond",
                        principalColumn: "PondId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LossShrimp",
                columns: table => new
                {
                    LossShrimpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LossShrimp", x => x.LossShrimpId);
                    table.ForeignKey(
                        name: "FK_LossShrimp_Pond_PondId",
                        column: x => x.PondId,
                        principalTable: "Pond",
                        principalColumn: "PondId",
                        onDelete: ReferentialAction.Cascade);
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
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.MedicineId);
                    table.ForeignKey(
                        name: "FK_Medicine_Pond_PondId",
                        column: x => x.PondId,
                        principalTable: "Pond",
                        principalColumn: "PondId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SizeShrimp",
                columns: table => new
                {
                    SizeShrimpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeShrimp", x => x.SizeShrimpId);
                    table.ForeignKey(
                        name: "FK_SizeShrimp_Pond_PondId",
                        column: x => x.PondId,
                        principalTable: "Pond",
                        principalColumn: "PondId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    CertificateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileData = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: true),
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CollectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.CertificateId);
                    table.ForeignKey(
                        name: "FK_Certificate_Collect_CollectId",
                        column: x => x.CollectId,
                        principalTable: "Collect",
                        principalColumn: "CollectId");
                    table.ForeignKey(
                        name: "FK_Certificate_Pond_PondId",
                        column: x => x.PondId,
                        principalTable: "Pond",
                        principalColumn: "PondId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_CollectId",
                table: "Certificate",
                column: "CollectId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_PondId",
                table: "Certificate",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_Collect_PondId",
                table: "Collect",
                column: "PondId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_PondId",
                table: "Food",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_LossShrimp_PondId",
                table: "LossShrimp",
                column: "PondId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_PondId",
                table: "Medicine",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_Pond_PondTypeId",
                table: "Pond",
                column: "PondTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SizeShrimp_PondId",
                table: "SizeShrimp",
                column: "PondId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "LossShrimp");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "SizeShrimp");

            migrationBuilder.DropTable(
                name: "Collect");

            migrationBuilder.DropTable(
                name: "Pond");

            migrationBuilder.DropTable(
                name: "PondType");
        }
    }
}
