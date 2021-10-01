using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Initials",
                schema: "g4s",
                table: "Teachers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "g4s",
                table: "Teachers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "g4s",
                table: "BehEventTypes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonMarks_Date",
                schema: "g4s",
                table: "StudentLessonMarks",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonMarks_StudentId",
                schema: "g4s",
                table: "StudentLessonMarks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_BehEventStudents_StudentId",
                schema: "g4s",
                table: "BehEventStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_BehEvents_EventDate",
                schema: "g4s",
                table: "BehEvents",
                column: "EventDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentLessonMarks_Date",
                schema: "g4s",
                table: "StudentLessonMarks");

            migrationBuilder.DropIndex(
                name: "IX_StudentLessonMarks_StudentId",
                schema: "g4s",
                table: "StudentLessonMarks");

            migrationBuilder.DropIndex(
                name: "IX_BehEventStudents_StudentId",
                schema: "g4s",
                table: "BehEventStudents");

            migrationBuilder.DropIndex(
                name: "IX_BehEvents_EventDate",
                schema: "g4s",
                table: "BehEvents");

            migrationBuilder.AlterColumn<string>(
                name: "Initials",
                schema: "g4s",
                table: "Teachers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "g4s",
                table: "Teachers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "g4s",
                table: "BehEventTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
