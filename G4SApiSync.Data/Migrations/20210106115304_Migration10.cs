using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentLessonMarks",
                schema: "g4s",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    ClassId = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    DataSet = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Academy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LessonMarkId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LessonAliasId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LessonMinutesLate = table.Column<int>(type: "int", nullable: true),
                    LessonNotes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLessonMarks", x => new { x.StudentId, x.Date, x.ClassId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentLessonMarks",
                schema: "g4s");
        }
    }
}
