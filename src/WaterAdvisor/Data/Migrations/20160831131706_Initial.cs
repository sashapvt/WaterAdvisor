using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterAdvisor.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Ba",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Ca",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Cl",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Colority",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "F",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Fe2",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Fe3",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HCO3",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "K",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mg",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mn",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NH4",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NO2",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NO3",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Na",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Odor",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Oxidability",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PO4",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SO4",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SiO2",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Sr",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TSS",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Taste",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Turbidity",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "pH",
                table: "Water",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ba",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Ca",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Cl",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Colority",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "F",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Fe2",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Fe3",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "HCO3",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "K",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Mg",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Mn",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "NH4",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "NO2",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "NO3",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Na",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Odor",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Oxidability",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "PO4",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "SO4",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "SiO2",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Sr",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "TSS",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Taste",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "Turbidity",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "pH",
                table: "Water");
        }
    }
}
