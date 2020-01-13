using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_EducationDetails_StudentId",
                schema: "g4s",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarkslotMarks_StudentId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetGrades_StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationDetails_Students_StudentId",
                schema: "g4s",
                table: "EducationDetails",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksheetGrades_Students_StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkslotMarks_Students_StudentId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationDetails_Students_StudentId",
                schema: "g4s",
                table: "EducationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MarksheetGrades_Students_StudentId",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkslotMarks_Students_StudentId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropIndex(
                name: "IX_MarkslotMarks_StudentId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropIndex(
                name: "IX_MarksheetGrades_StudentId",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_EducationDetails_StudentId",
                schema: "g4s",
                table: "Students",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "EducationDetails",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
