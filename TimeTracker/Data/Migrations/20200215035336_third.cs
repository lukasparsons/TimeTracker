using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTracker.Data.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Workweeks_WorkWeekId",
                table: "Workdays");

            migrationBuilder.DropIndex(
                name: "IX_Workdays_WorkWeekId",
                table: "Workdays");

            migrationBuilder.DropColumn(
                name: "WorkWeekId",
                table: "Workdays");

            migrationBuilder.AddColumn<int>(
                name: "WeekId",
                table: "Workdays",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "WorkWeekId",
                table: "Workdays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workdays_WorkWeekId",
                table: "Workdays",
                column: "WorkWeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Workweeks_WorkWeekId",
                table: "Workdays",
                column: "WorkWeekId",
                principalTable: "Workweeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
