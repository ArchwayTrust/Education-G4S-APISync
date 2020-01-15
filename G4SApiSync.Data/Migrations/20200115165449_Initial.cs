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
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    AttributeName = table.Column<string>(maxLength: 500, nullable: true),
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
                name: "GradeTypes",
                schema: "g4s",
                columns: table => new
                {
                    GradeTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeTypes", x => x.GradeTypeId);
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
                });

            migrationBuilder.CreateTable(
                name: "SyncResults",
                schema: "g4s",
                columns: table => new
                {
                    SyncResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoggedAt = table.Column<DateTime>(nullable: false),
                    AcademyCode = table.Column<string>(maxLength: 500, nullable: true),
                    EndPoint = table.Column<string>(nullable: true),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    YearGroup = table.Column<int>(nullable: true),
                    Result = table.Column<bool>(nullable: false),
                    Exception = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncResults", x => x.SyncResultId);
                });

            migrationBuilder.CreateTable(
                name: "AcademySecurity",
                schema: "sec",
                columns: table => new
                {
                    AcademyCode = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    CurrentAcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    APIKey = table.Column<string>(maxLength: 100, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    LowestYear = table.Column<int>(nullable: false),
                    HighestYear = table.Column<int>(nullable: false)
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
                name: "GradeNames",
                schema: "g4s",
                columns: table => new
                {
                    GradeNameId = table.Column<string>(maxLength: 100, nullable: false),
                    GradeTypeId = table.Column<int>(nullable: false),
                    AcademicYear = table.Column<string>(maxLength: 100, nullable: true),
                    Academy = table.Column<string>(maxLength: 100, nullable: true),
                    NCYear = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    ShortName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    PreferredProgressGrade = table.Column<bool>(nullable: false),
                    PreferredTargetGrade = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeNames", x => x.GradeNameId);
                    table.ForeignKey(
                        name: "FK_GradeNames_GradeTypes_GradeTypeId",
                        column: x => x.GradeTypeId,
                        principalSchema: "g4s",
                        principalTable: "GradeTypes",
                        principalColumn: "GradeTypeId",
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
                    Value = table.Column<string>(maxLength: 1000, nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
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
                    NCYear = table.Column<string>(maxLength: 20, nullable: true),
                    RegistrationGroup = table.Column<string>(maxLength: 100, nullable: true),
                    House = table.Column<string>(maxLength: 500, nullable: true),
                    AdmissionDate = table.Column<DateTime>(type: "Date", nullable: true),
                    LeavingDate = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDetails", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_EducationDetails_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriorAttainment",
                schema: "g4s",
                columns: table => new
                {
                    StudentId = table.Column<string>(maxLength: 100, nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    AcademicYear = table.Column<string>(maxLength: 100, nullable: true),
                    Academy = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Value = table.Column<string>(maxLength: 100, nullable: true),
                    ValueAcademicYear = table.Column<string>(maxLength: 100, nullable: true),
                    ValueDate = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorAttainment", x => new { x.StudentId, x.Code });
                    table.ForeignKey(
                        name: "FK_PriorAttainment_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "g4s",
                columns: table => new
                {
                    GroupId = table.Column<string>(maxLength: 100, nullable: false),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Academy = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 1000, nullable: true),
                    Code = table.Column<string>(maxLength: 1000, nullable: true),
                    SubjectId = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "g4s",
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
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
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
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
                name: "GroupStudents",
                schema: "g4s",
                columns: table => new
                {
                    GroupId = table.Column<string>(maxLength: 100, nullable: false),
                    StudentId = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStudents", x => new { x.StudentId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupStudents_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "g4s",
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarksheetGrades",
                schema: "g4s",
                columns: table => new
                {
                    MarksheetId = table.Column<string>(nullable: false),
                    StudentId = table.Column<string>(nullable: false),
                    Grade = table.Column<string>(nullable: true),
                    Alias = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarksheetGrades", x => new { x.StudentId, x.MarksheetId });
                    table.ForeignKey(
                        name: "FK_MarksheetGrades_Marksheets_MarksheetId",
                        column: x => x.MarksheetId,
                        principalSchema: "g4s",
                        principalTable: "Marksheets",
                        principalColumn: "MarksheetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarksheetGrades_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Markslots",
                schema: "g4s",
                columns: table => new
                {
                    MarkslotId = table.Column<string>(maxLength: 100, nullable: false),
                    MarksheetId = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAttributeValues",
                schema: "g4s",
                columns: table => new
                {
                    StudentAttributeId = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(maxLength: 1000, nullable: true),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Date = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttributeValues", x => x.StudentAttributeId);
                    table.ForeignKey(
                        name: "FK_StudentAttributeValues_StudentAttributes_StudentAttributeId",
                        column: x => x.StudentAttributeId,
                        principalSchema: "g4s",
                        principalTable: "StudentAttributes",
                        principalColumn: "StudentAttributeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarkslotMarks",
                schema: "g4s",
                columns: table => new
                {
                    MarkslotId = table.Column<string>(maxLength: 100, nullable: false),
                    StudentId = table.Column<string>(maxLength: 100, nullable: false),
                    Grade = table.Column<string>(maxLength: 50, nullable: true),
                    Alias = table.Column<string>(maxLength: 50, nullable: true),
                    Mark = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkslotMarks", x => new { x.StudentId, x.MarkslotId });
                    table.ForeignKey(
                        name: "FK_MarkslotMarks_Markslots_MarkslotId",
                        column: x => x.MarkslotId,
                        principalSchema: "g4s",
                        principalTable: "Markslots",
                        principalColumn: "MarkslotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarkslotMarks_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "g4s",
                table: "GradeTypes",
                columns: new[] { "GradeTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "External target" },
                    { 2, "Teacher target" },
                    { 3, "Combined target" },
                    { 4, "Current" },
                    { 5, "Project" },
                    { 6, "Actual" },
                    { 7, "Honest" },
                    { 8, "Aspirational" },
                    { 9, "Additional target" },
                    { 10, "Baseline grade" }
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
                name: "IX_GradeNames_GradeTypeId",
                schema: "g4s",
                table: "GradeNames",
                column: "GradeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SubjectId",
                schema: "g4s",
                table: "Groups",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudents_GroupId",
                schema: "g4s",
                table: "GroupStudents",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetGrades_MarksheetId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "MarksheetId");

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
                name: "GradeNames",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "GroupStudents",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "MarksheetGrades",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "MarkslotMarks",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "PriorAttainment",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "StudentAttributeValues",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "SyncResults",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "AcademySecurity",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "AttributeTypes",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "GradeTypes",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "Markslots",
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
                name: "Students",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "g4s");
        }
    }
}
