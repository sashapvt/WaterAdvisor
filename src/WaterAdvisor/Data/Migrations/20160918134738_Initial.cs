using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WaterAdvisor.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Water",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ba = table.Column<double>(nullable: false),
                    Ca = table.Column<double>(nullable: false),
                    Cl = table.Column<double>(nullable: false),
                    Colority = table.Column<double>(nullable: false),
                    F = table.Column<double>(nullable: false),
                    Fe2 = table.Column<double>(nullable: false),
                    Fe3 = table.Column<double>(nullable: false),
                    HCO3 = table.Column<double>(nullable: false),
                    K = table.Column<double>(nullable: false),
                    Mg = table.Column<double>(nullable: false),
                    Mn = table.Column<double>(nullable: false),
                    NH4 = table.Column<double>(nullable: false),
                    NO2 = table.Column<double>(nullable: false),
                    NO3 = table.Column<double>(nullable: false),
                    Na = table.Column<double>(nullable: false),
                    Odor = table.Column<double>(nullable: false),
                    Oxidability = table.Column<double>(nullable: false),
                    PO4 = table.Column<double>(nullable: false),
                    SO4 = table.Column<double>(nullable: false),
                    SiO2 = table.Column<double>(nullable: false),
                    Sr = table.Column<double>(nullable: false),
                    TSS = table.Column<double>(nullable: false),
                    Taste = table.Column<double>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    Turbidity = table.Column<double>(nullable: false),
                    pH = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Water", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectComment = table.Column<string>(maxLength: 255, nullable: true),
                    ProjectDate = table.Column<DateTime>(nullable: false),
                    ProjectName = table.Column<string>(maxLength: 100, nullable: true),
                    RecoveryRO = table.Column<double>(nullable: false),
                    UserId = table.Column<string>(maxLength: 450, nullable: true),
                    WaterInId = table.Column<int>(nullable: true),
                    pHCorrected = table.Column<double>(nullable: false),
                    pHCorrection = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Water_WaterInId",
                        column: x => x.WaterInId,
                        principalTable: "Water",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_WaterInId",
                table: "Project",
                column: "WaterInId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Water");
        }
    }
}
