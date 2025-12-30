using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMdataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddMaintenanceSche : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Maintenances",
                newName: "EstimatedCost");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Maintenances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MechanicPhone",
                table: "Maintenances",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentMileage",
                table: "Maintenances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Maintenances",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "System",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System");

            migrationBuilder.AddColumn<decimal>(
                name: "ActualCost",
                table: "Maintenances",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Maintenances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Maintenances",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualCost",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Maintenances");

            migrationBuilder.RenameColumn(
                name: "EstimatedCost",
                table: "Maintenances",
                newName: "Cost");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Maintenances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MechanicPhone",
                table: "Maintenances",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentMileage",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Maintenances",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "System");
        }
    }
}
