using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTracker.Data.Migrations
{
    public partial class Seod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Workweeks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workweeks_WorkerId",
                table: "Workweeks",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workweeks_Workers_WorkerId",
                table: "Workweeks",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workweeks_Workers_WorkerId",
                table: "Workweeks");

            migrationBuilder.DropIndex(
                name: "IX_Workweeks_WorkerId",
                table: "Workweeks");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Workweeks");
        }
    }
}
