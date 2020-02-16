using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTracker.Data.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Workweeks_WeekId",
                table: "Workdays");

            migrationBuilder.DropIndex(
                name: "IX_Workdays_WeekId",
                table: "Workdays");

            migrationBuilder.DropColumn(
                name: "WeekId",
                table: "Workdays");

            migrationBuilder.AddColumn<int>(
                name: "WorkweekId",
                table: "Workdays",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workdays_WorkweekId",
                table: "Workdays",
                column: "WorkweekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Workweeks_WorkweekId",
                table: "Workdays",
                column: "WorkweekId",
                principalTable: "Workweeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Workweeks_WorkweekId",
                table: "Workdays");

            migrationBuilder.DropIndex(
                name: "IX_Workdays_WorkweekId",
                table: "Workdays");

            migrationBuilder.DropColumn(
                name: "WorkweekId",
                table: "Workdays");

            migrationBuilder.AddColumn<int>(
                name: "WeekId",
                table: "Workdays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workdays_WeekId",
                table: "Workdays",
                column: "WeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Workweeks_WeekId",
                table: "Workdays",
                column: "WeekId",
                principalTable: "Workweeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
