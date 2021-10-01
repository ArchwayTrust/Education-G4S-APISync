using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentSessionMarks",
                schema: "g4s",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    DataSet = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Academy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SessionMarkId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SessionAliasId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SessionMinutesLates = table.Column<int>(type: "int", nullable: false),
                    SessionNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSessionMarks", x => new { x.StudentId, x.Date, x.Session });
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSessionMarks_Academy_DataSet",
                schema: "g4s",
                table: "StudentSessionMarks",
                columns: new[] { "Academy", "DataSet" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSessionMarks_Date_Session",
                schema: "g4s",
                table: "StudentSessionMarks",
                columns: new[] { "Date", "Session" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSessionMarks_StudentId",
                schema: "g4s",
                table: "StudentSessionMarks",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSessionMarks",
                schema: "g4s");
        }
    }
}
