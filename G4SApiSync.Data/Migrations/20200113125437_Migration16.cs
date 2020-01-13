using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration16 : Migration
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
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
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
                name: "FK_MarksheetGrades_Students_StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkslotMarks_Students_StudentId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
