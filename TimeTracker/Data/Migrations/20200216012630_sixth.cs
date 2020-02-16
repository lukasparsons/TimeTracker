using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTracker.Data.Migrations
{
    public partial class sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "In",
                table: "Workdays");

            migrationBuilder.DropColumn(
                name: "Out",
                table: "Workdays");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClockIn",
                table: "Workdays",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClockOut",
                table: "Workdays",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockIn",
                table: "Workdays");

            migrationBuilder.DropColumn(
                name: "ClockOut",
                table: "Workdays");

            migrationBuilder.AddColumn<DateTime>(
                name: "In",
                table: "Workdays",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Out",
                table: "Workdays",
                type: "datetime2",
                nullable: true);
        }
    }
}
