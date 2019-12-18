using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sec");

            migrationBuilder.EnsureSchema(
                name: "g4s");

            migrationBuilder.CreateTable(
                name: "AttributeTypes",
                schema: "g4s",
                columns: table => new
                {
                    AttributeTypeId = table.Column<string>(maxLength: 100, nullable: false),
                    G4SAttributeId = table.Column<int>(nullable: false),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Academy = table.Column<string>(maxLength: 10, nullable: true),
                    AttributeGroup = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    AttributeName = table.Column<string>(maxLength: 300, nullable: true),
                    IsSystem = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeTypes", x => x.AttributeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "g4s",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(maxLength: 100, nullable: false),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Academy = table.Column<string>(maxLength: 10, nullable: true),
                    G4SDepartmentId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "EducationDetails",
                schema: "g4s",
                columns: table => new
                {
                    StudentId = table.Column<string>(maxLength: 100, nullable: false),
                    G4SStuId = table.Column<int>(nullable: false),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Academy = table.Column<string>(maxLength: 10, nullable: true),
                    UPN = table.Column<string>(maxLength: 13, nullable: true),
                    FormerUPN = table.Column<string>(maxLength: 13, nullable: true),
                    NCYear = table.Column<string>(maxLength: 4, nullable: true),
                    RegistrationGroup = table.Column<string>(maxLength: 50, nullable: true),
                    House = table.Column<string>(maxLength: 200, nullable: true),
                    AdmissionDate = table.Column<DateTime>(type: "Date", nullable: true),
                    LeavingDate = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDetails", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "AcademySecurity",
                schema: "sec",
                columns: table => new
                {
                    AcademyCode = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CurrentAcademicYear = table.Column<string>(nullable: true),
                    APIKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademySecurity", x => x.AcademyCode);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                schema: "g4s",
                columns: table => new
                {
                    SubjectId = table.Column<string>(maxLength: 100, nullable: false),
                    G4SSubjectId = table.Column<int>(nullable: false),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Academy = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    YearGroup = table.Column<string>(maxLength: 50, nullable: true),
                    DepartmentId = table.Column<string>(maxLength: 100, nullable: true),
                    QAN = table.Column<string>(maxLength: 50, nullable: true),
                    QualificationTitle = table.Column<string>(maxLength: 300, nullable: true),
                    QualificationSchemeName = table.Column<string>(maxLength: 300, nullable: true),
                    IncludeInStats = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_Subjects_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "g4s",
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAttributes",
                schema: "g4s",
                columns: table => new
                {
                    StudentAttributeId = table.Column<string>(maxLength: 100, nullable: false),
                    StudentId = table.Column<string>(maxLength: 100, nullable: true),
                    G4SStuId = table.Column<int>(nullable: false),
                    AttributeId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    IsSystem = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttributes", x => x.StudentAttributeId);
                    table.ForeignKey(
                        name: "FK_StudentAttributes_EducationDetails_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "EducationDetails",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                schema: "g4s",
                columns: table => new
                {
                    StudentId = table.Column<string>(maxLength: 100, nullable: false),
                    G4SStuId = table.Column<int>(nullable: false),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Academy = table.Column<string>(maxLength: 10, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "Date", nullable: false),
                    Sex = table.Column<string>(maxLength: 1, nullable: true),
                    LegalFirstName = table.Column<string>(maxLength: 200, nullable: true),
                    LegalLastName = table.Column<string>(maxLength: 200, nullable: true),
                    PreferredFirstName = table.Column<string>(maxLength: 200, nullable: true),
                    PreferredLastName = table.Column<string>(maxLength: 200, nullable: true),
                    MiddleNames = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_EducationDetails_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "EducationDetails",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marksheets",
                schema: "g4s",
                columns: table => new
                {
                    MarksheetId = table.Column<string>(maxLength: 100, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 100, nullable: true),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Academy = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marksheets", x => x.MarksheetId);
                    table.ForeignKey(
                        name: "FK_Marksheets_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "g4s",
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentAttributeValues",
                schema: "g4s",
                columns: table => new
                {
                    StudentAttributeValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentAttributeId = table.Column<string>(maxLength: 100, nullable: true),
                    Value = table.Column<string>(maxLength: 300, nullable: true),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Date = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttributeValues", x => x.StudentAttributeValueId);
                    table.ForeignKey(
                        name: "FK_StudentAttributeValues_StudentAttributes_StudentAttributeId",
                        column: x => x.StudentAttributeId,
                        principalSchema: "g4s",
                        principalTable: "StudentAttributes",
                        principalColumn: "StudentAttributeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeValues",
                schema: "g4s",
                columns: table => new
                {
                    AttributeValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeTypeId = table.Column<string>(maxLength: 100, nullable: true),
                    StudentId = table.Column<string>(maxLength: 100, nullable: true),
                    Value = table.Column<string>(maxLength: 500, nullable: true),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Date = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValues", x => x.AttributeValueId);
                    table.ForeignKey(
                        name: "FK_AttributeValues_AttributeTypes_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalSchema: "g4s",
                        principalTable: "AttributeTypes",
                        principalColumn: "AttributeTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeValues_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarksheetGrades",
                schema: "g4s",
                columns: table => new
                {
                    MarksheetGradeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarksheetId = table.Column<int>(nullable: false),
                    StudentId = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    Mark = table.Column<float>(nullable: false),
                    MarksheetId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarksheetGrades", x => x.MarksheetGradeId);
                    table.ForeignKey(
                        name: "FK_MarksheetGrades_Marksheets_MarksheetId1",
                        column: x => x.MarksheetId1,
                        principalSchema: "g4s",
                        principalTable: "Marksheets",
                        principalColumn: "MarksheetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarksheetGrades_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Markslots",
                schema: "g4s",
                columns: table => new
                {
                    MarkslotId = table.Column<string>(maxLength: 100, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 100, nullable: true),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    MarksheetId = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markslots", x => x.MarkslotId);
                    table.ForeignKey(
                        name: "FK_Markslots_Marksheets_MarksheetId",
                        column: x => x.MarksheetId,
                        principalSchema: "g4s",
                        principalTable: "Marksheets",
                        principalColumn: "MarksheetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarkslotMarks",
                schema: "g4s",
                columns: table => new
                {
                    MarkslotMarkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MockslotId = table.Column<string>(maxLength: 100, nullable: true),
                    StudentId = table.Column<string>(maxLength: 100, nullable: true),
                    SubjectId = table.Column<string>(maxLength: 100, nullable: true),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Grade = table.Column<string>(maxLength: 50, nullable: true),
                    Alias = table.Column<string>(maxLength: 50, nullable: true),
                    Mark = table.Column<float>(nullable: false),
                    MarkslotId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkslotMarks", x => x.MarkslotMarkId);
                    table.ForeignKey(
                        name: "FK_MarkslotMarks_Markslots_MarkslotId",
                        column: x => x.MarkslotId,
                        principalSchema: "g4s",
                        principalTable: "Markslots",
                        principalColumn: "MarkslotId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarkslotMarks_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_AttributeTypeId",
                schema: "g4s",
                table: "AttributeValues",
                column: "AttributeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_StudentId",
                schema: "g4s",
                table: "AttributeValues",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetGrades_MarksheetId1",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "MarksheetId1");

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetGrades_StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Marksheets_SubjectId",
                schema: "g4s",
                table: "Marksheets",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkslotMarks_MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "MarkslotId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkslotMarks_StudentId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Markslots_MarksheetId",
                schema: "g4s",
                table: "Markslots",
                column: "MarksheetId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttributes_StudentId",
                schema: "g4s",
                table: "StudentAttributes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttributeValues_StudentAttributeId",
                schema: "g4s",
                table: "StudentAttributeValues",
                column: "StudentAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_DepartmentId",
                schema: "g4s",
                table: "Subjects",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeValues",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "MarksheetGrades",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "MarkslotMarks",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "StudentAttributeValues",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "AcademySecurity",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "AttributeTypes",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "Markslots",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "Students",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "StudentAttributes",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "Marksheets",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "EducationDetails",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "Subjects",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "g4s");
        }
    }
}
