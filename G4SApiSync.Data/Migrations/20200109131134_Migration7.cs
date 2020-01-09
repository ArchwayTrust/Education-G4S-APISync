using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarksheetGrades_Students_StudentId",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksheetGrades_Students_StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarksheetGrades_Students_StudentId",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksheetGrades_Students_StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
