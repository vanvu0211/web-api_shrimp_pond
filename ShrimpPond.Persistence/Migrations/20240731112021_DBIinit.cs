using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class DBIinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.MedicineId);
                });

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
                    Deep = table.Column<float>(type: "real", nullable: false),
                    Diameter = table.Column<float>(type: "real", nullable: false),
                    PondTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PondTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OriginPondId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeedId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountShrimp = table.Column<float>(type: "real", nullable: false),
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
                name: "EnvironmentStatus",
                columns: table => new
                {
                    EnvironmentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentStatus", x => x.EnvironmentStatusId);
                    table.ForeignKey(
                        name: "FK_EnvironmentStatus_Pond_PondId",
                        column: x => x.PondId,
                        principalTable: "Pond",
                        principalColumn: "PondId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodFeeding",
                columns: table => new
                {
                    FoodFeedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodFeeding", x => x.FoodFeedingId);
                    table.ForeignKey(
                        name: "FK_FoodFeeding_Pond_PondId",
                        column: x => x.PondId,
                        principalTable: "Pond",
                        principalColumn: "PondId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Harvest",
                columns: table => new
                {
                    HarvestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HarvestTime = table.Column<int>(type: "int", nullable: false),
                    HarvestType = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<float>(type: "real", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    HarvestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harvest", x => x.HarvestId);
                    table.ForeignKey(
                        name: "FK_Harvest_Pond_PondId",
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
                    LossValue = table.Column<float>(type: "real", nullable: false),
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
                name: "MedicineFeeding",
                columns: table => new
                {
                    MedicineFeedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineFeeding", x => x.MedicineFeedingId);
                    table.ForeignKey(
                        name: "FK_MedicineFeeding_Pond_PondId",
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
                    SizeValue = table.Column<float>(type: "real", nullable: false),
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
                name: "FoodForFeeding",
                columns: table => new
                {
                    FoodForFeedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    FoodFeedingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodForFeeding", x => x.FoodForFeedingId);
                    table.ForeignKey(
                        name: "FK_FoodForFeeding_FoodFeeding_FoodFeedingId",
                        column: x => x.FoodFeedingId,
                        principalTable: "FoodFeeding",
                        principalColumn: "FoodFeedingId",
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
                    HarvestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.CertificateId);
                    table.ForeignKey(
                        name: "FK_Certificate_Harvest_HarvestId",
                        column: x => x.HarvestId,
                        principalTable: "Harvest",
                        principalColumn: "HarvestId");
                    table.ForeignKey(
                        name: "FK_Certificate_Pond_PondId",
                        column: x => x.PondId,
                        principalTable: "Pond",
                        principalColumn: "PondId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineForFeeding",
                columns: table => new
                {
                    MedicineForFeedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    MedicineFeedingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineForFeeding", x => x.MedicineForFeedingId);
                    table.ForeignKey(
                        name: "FK_MedicineForFeeding_MedicineFeeding_MedicineFeedingId",
                        column: x => x.MedicineFeedingId,
                        principalTable: "MedicineFeeding",
                        principalColumn: "MedicineFeedingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_HarvestId",
                table: "Certificate",
                column: "HarvestId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_PondId",
                table: "Certificate",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentStatus_PondId",
                table: "EnvironmentStatus",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodFeeding_PondId",
                table: "FoodFeeding",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodForFeeding_FoodFeedingId",
                table: "FoodForFeeding",
                column: "FoodFeedingId");

            migrationBuilder.CreateIndex(
                name: "IX_Harvest_PondId",
                table: "Harvest",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_LossShrimp_PondId",
                table: "LossShrimp",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineFeeding_PondId",
                table: "MedicineFeeding",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineForFeeding_MedicineFeedingId",
                table: "MedicineForFeeding",
                column: "MedicineFeedingId");

            migrationBuilder.CreateIndex(
                name: "IX_Pond_PondTypeId",
                table: "Pond",
                column: "PondTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SizeShrimp_PondId",
                table: "SizeShrimp",
                column: "PondId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "EnvironmentStatus");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "FoodForFeeding");

            migrationBuilder.DropTable(
                name: "LossShrimp");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "MedicineForFeeding");

            migrationBuilder.DropTable(
                name: "SizeShrimp");

            migrationBuilder.DropTable(
                name: "Harvest");

            migrationBuilder.DropTable(
                name: "FoodFeeding");

            migrationBuilder.DropTable(
                name: "MedicineFeeding");

            migrationBuilder.DropTable(
                name: "Pond");

            migrationBuilder.DropTable(
                name: "PondType");
        }
    }
}
