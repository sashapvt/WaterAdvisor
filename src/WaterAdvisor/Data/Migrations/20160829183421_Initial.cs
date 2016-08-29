using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterAdvisor.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Water_Project_ProjectId",
                table: "Water");

            migrationBuilder.DropIndex(
                name: "IX_Water_ProjectId",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Water");

            migrationBuilder.AddColumn<int>(
                name: "WaterInId",
                table: "Project",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_WaterInId",
                table: "Project",
                column: "WaterInId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Water_WaterInId",
                table: "Project",
                column: "WaterInId",
                principalTable: "Water",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Water_WaterInId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_WaterInId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "WaterInId",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Water",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Water_ProjectId",
                table: "Water",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Water_Project_ProjectId",
                table: "Water",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
