﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class addmachinetable123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PondIds_Machines_machineId",
                table: "PondIds");

            migrationBuilder.DropForeignKey(
                name: "FK_PondIds_Machines_machineId1",
                table: "PondIds");

            migrationBuilder.DropIndex(
                name: "IX_PondIds_machineId1",
                table: "PondIds");

            migrationBuilder.DropColumn(
                name: "machineId1",
                table: "PondIds");

            migrationBuilder.AddForeignKey(
                name: "FK_PondIds_Machines_machineId",
                table: "PondIds",
                column: "machineId",
                principalTable: "Machines",
                principalColumn: "machineId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PondIds_Machines_machineId",
                table: "PondIds");

            migrationBuilder.AddColumn<int>(
                name: "machineId1",
                table: "PondIds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PondIds_machineId1",
                table: "PondIds",
                column: "machineId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PondIds_Machines_machineId",
                table: "PondIds",
                column: "machineId",
                principalTable: "Machines",
                principalColumn: "machineId");

            migrationBuilder.AddForeignKey(
                name: "FK_PondIds_Machines_machineId1",
                table: "PondIds",
                column: "machineId1",
                principalTable: "Machines",
                principalColumn: "machineId");
        }
    }
}
