using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarksheetGrades_Students_StudentId",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkslotMarks_Students_StudentId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksheetGrades_Students_StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkslotMarks_Students_StudentId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarksheetGrades_Students_StudentId",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkslotMarks_Students_StudentId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksheetGrades_Students_StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkslotMarks_Students_StudentId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
