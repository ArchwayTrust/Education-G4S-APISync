using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamResults",
                schema: "g4s",
                columns: table => new
                {
                    ExamResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(maxLength: 100, nullable: true),
                    AcademicYear = table.Column<string>(maxLength: 10, nullable: true),
                    Academy = table.Column<string>(maxLength: 10, nullable: true),
                    NCYear = table.Column<int>(nullable: false),
                    ExamAcademicYear = table.Column<string>(maxLength: 10, nullable: true),
                    QAN = table.Column<string>(maxLength: 13, nullable: true),
                    QualificationTitle = table.Column<string>(maxLength: 200, nullable: true),
                    ExamDate = table.Column<DateTime>(type: "Date", nullable: true),
                    Grade = table.Column<string>(maxLength: 200, nullable: true),
                    KS123Literal = table.Column<string>(maxLength: 100, nullable: true),
                    SubjectId = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults", x => x.ExamResultId);
                    table.ForeignKey(
                        name: "FK_ExamResults_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamResults_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "g4s",
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_StudentId",
                schema: "g4s",
                table: "ExamResults",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_SubjectId",
                schema: "g4s",
                table: "ExamResults",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamResults",
                schema: "g4s");
        }
    }
}
